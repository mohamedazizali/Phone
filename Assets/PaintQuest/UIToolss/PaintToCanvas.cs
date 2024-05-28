using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PaintToCanvas : MonoBehaviour
{
    public Sprite baseSprite;
    public RawImage paintedImage;
    public GameObject canvasModel; // 3D model representing the canvas

    private RenderTexture combinedTexture;

    public void CaptureAndApply()
    {
        // Convert the base sprite to a Texture2D
        Texture2D baseTexture = SpriteToTexture(baseSprite);

        // Capture the painted texture
        Texture2D paintedTexture = CapturePaintedTexture();

        // Combine with the base image
        Texture2D combinedTexture = CombineTextures(baseTexture, paintedTexture);

        // Apply to 3D model
        ApplyToCanvasModel(combinedTexture);
    }

    private Texture2D SpriteToTexture(Sprite sprite)
    {
        // Create a new Texture2D
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        texture.SetPixels(sprite.texture.GetPixels((int)sprite.rect.x, (int)sprite.rect.y,
                          (int)sprite.rect.width, (int)sprite.rect.height));
        texture.Apply();
        return texture;
    }

    private Texture2D CapturePaintedTexture()
    {
        // Create a temporary RenderTexture
        RenderTexture tempRT = RenderTexture.GetTemporary(paintedImage.texture.width, paintedImage.texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);

        // Set the RawImage's texture as the active RenderTexture
        Graphics.Blit(paintedImage.texture, tempRT);

        // Create a new Texture2D to read pixels into
        Texture2D paintedTexture = new Texture2D(paintedImage.texture.width, paintedImage.texture.height);

        // Read pixels from the active RenderTexture
        RenderTexture.active = tempRT;
        paintedTexture.ReadPixels(new Rect(0, 0, paintedImage.texture.width, paintedImage.texture.height), 0, 0);
        paintedTexture.Apply();

        // Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tempRT);

        return paintedTexture;
    }



    private Texture2D CombineTextures(Texture2D baseTexture, Texture2D paintedTexture)
    {
        // Ensure both textures have the same dimensions
        if (baseTexture.width != paintedTexture.width || baseTexture.height != paintedTexture.height)
        {
            Debug.LogWarning("Textures have different dimensions, resizing painted texture to match base texture.");

            // Resize the painted texture to match the dimensions of the base texture
            paintedTexture = ResizeTexture(paintedTexture, baseTexture.width, baseTexture.height);

            if (paintedTexture == null)
            {
                Debug.LogError("Failed to resize painted texture.");
                return null;
            }
        }

        // Create a new texture
        Texture2D combinedTexture = new Texture2D(baseTexture.width, baseTexture.height);

        // Copy the pixels from the base texture to the combined texture
        combinedTexture.SetPixels32(baseTexture.GetPixels32());

        // Apply the painted texture onto the combined texture
        Color32[] paintedPixels = paintedTexture.GetPixels32();
        for (int i = 0; i < paintedPixels.Length; i++)
        {
            if (paintedPixels[i].a > 0)
            {
                combinedTexture.SetPixel(i % baseTexture.width, i / baseTexture.width, paintedPixels[i]);
            }
        }

        // Apply changes and return the combined texture
        combinedTexture.Apply();
        return combinedTexture;
    }

    private Texture2D ResizeTexture(Texture2D texture, int newWidth, int newHeight)
    {
        if (texture == null)
        {
            Debug.LogError("Cannot resize null texture.");
            return null;
        }

        // Create a new texture with the desired dimensions
        Texture2D resizedTexture = new Texture2D(newWidth, newHeight);

        // Copy the pixels from the original texture to the resized texture
        for (int y = 0; y < newHeight; y++)
        {
            for (int x = 0; x < newWidth; x++)
            {
                resizedTexture.SetPixel(x, y, texture.GetPixelBilinear((float)x / newWidth, (float)y / newHeight));
            }
        }

        resizedTexture.Apply();
        return resizedTexture;
    }




    private void ApplyToCanvasModel(Texture2D texture)
    {
        // Apply the combined texture to the 3D model representing the canvas
        Renderer canvasRenderer = canvasModel.GetComponent<Renderer>();
        canvasRenderer.material.mainTexture = texture;
    }
}

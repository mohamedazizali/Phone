using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DrawMesh : MonoBehaviour
{
    public RawImage rawImage;
    public int textureWidth = 512;
    public int textureHeight = 512;
    public float brushSize = 10f;
    public Color brushColor = Color.black;
    public GameObject canvasObjectPrefab; // Prefab of the canvas object in the environment

    private Texture2D canvasTexture;
    private RectTransform rawImageRectTransform;
    private bool isPainting = false;

    void Start()
    {
        InitializeCanvasTexture();
        rawImage.texture = canvasTexture;
        rawImageRectTransform = rawImage.rectTransform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPainting = true;
            DrawOnTexture();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPainting = false;
        }

        if (isPainting)
        {
            DrawOnTexture();
        }
        if (gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void InitializeCanvasTexture()
    {
        canvasTexture = new Texture2D(textureWidth, textureHeight);
        Color32[] transparentPixels = new Color32[textureWidth * textureHeight];
        for (int i = 0; i < transparentPixels.Length; i++)
        {
            transparentPixels[i] = new Color32(0, 0, 0, 0);
        }
        canvasTexture.SetPixels32(transparentPixels);
        canvasTexture.Apply();
    }

    void DrawOnTexture()
    {
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rawImageRectTransform, Input.mousePosition, null, out localMousePosition);

        // Convert local mouse position to texture coordinates
        float textureX = (localMousePosition.x + rawImageRectTransform.rect.width / 2) * (textureWidth / rawImageRectTransform.rect.width);
        float textureY = (localMousePosition.y + rawImageRectTransform.rect.height / 2) * (textureHeight / rawImageRectTransform.rect.height);

        // Round to integer values
        int x = Mathf.RoundToInt(textureX);
        int y = Mathf.RoundToInt(textureY);

        if (x >= 0 && x < textureWidth && y >= 0 && y < textureHeight)
        {
            DrawCircle(canvasTexture, x, y, (int)brushSize, brushColor);
            canvasTexture.Apply();
        }
    }

    void DrawCircle(Texture2D texture, int centerX, int centerY, int radius, Color color)
    {
        int x, y, px, nx, py, ny, d;

        for (x = 0; x <= radius; x++)
        {
            d = (int)Mathf.Ceil(Mathf.Sqrt(radius * radius - x * x));

            for (y = 0; y <= d; y++)
            {
                px = centerX + x;
                nx = centerX - x;
                py = centerY + y;
                ny = centerY - y;

                if (px >= 0 && px < textureWidth && py >= 0 && py < textureHeight)
                {
                    texture.SetPixel(px, py, color);
                }
                if (nx >= 0 && nx < textureWidth && py >= 0 && py < textureHeight)
                {
                    texture.SetPixel(nx, py, color);
                }
                if (px >= 0 && px < textureWidth && ny >= 0 && ny < textureHeight)
                {
                    texture.SetPixel(px, ny, color);
                }
                if (nx >= 0 && nx < textureWidth && ny >= 0 && ny < textureHeight)
                {
                    texture.SetPixel(nx, ny, color);
                }
            }
        }
    }

    public void SavePaintedTexture(string filePath)
    {
        byte[] bytes = canvasTexture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
    }
    public void SetBrushColor(Color color)
    {
        brushColor = color;
    }

    public void ChangeBrushSize(float amount)
    {
        brushSize += amount;
        // Ensure brush size stays within the range of 1 to 15
        brushSize = Mathf.Clamp(brushSize, 1f, 15f);
    }


    public void GenerateCanvasObject()
    {
        string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        // Save the painted texture
        string filePath = Path.Combine(documentsPath, "PaintedTexture.png");
        SavePaintedTexture(filePath);

        // Load the painted texture
        Texture2D paintedTexture = new Texture2D(textureWidth, textureHeight);
        byte[] bytes = File.ReadAllBytes(filePath);
        paintedTexture.LoadImage(bytes);

        // Instantiate a canvas object in the environment and apply the painted texture
        GameObject canvasObject = Instantiate(canvasObjectPrefab, Vector3.zero, Quaternion.identity);
        Renderer renderer = canvasObject.GetComponent<Renderer>();
        renderer.material.mainTexture = paintedTexture;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame, polaroid;
    float maxPictures = 0f;
    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation;
    public CameraChange camchange;
    private Texture2D screenCapture;
    private bool viewingPhoto;
    [SerializeField] private bool screenshotOfNPC;

    [SerializeField] private InventoryManager inventoryManager2;
    // Layer mask for raycasting to detect NPCs
    [SerializeField] private LayerMask npcLayerMask;
    [SerializeField] private float wincon = 0f;
    [SerializeField] bool questCompleted = false;
    [SerializeField] private AudioSource camsound;
    [SerializeField] private AudioClip startSound;

    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        camsound = gameObject.GetComponent<AudioSource>();

    }


    private void Update()
    {
        if (InventoryManager.Instance.HasItem("Camera"))
        {
            if (Input.GetMouseButtonDown(0) && camchange.CamMode == 1 && maxPictures < 6)
            {
                if (!viewingPhoto)
                {
                    StartCoroutine(CapturePhoto());
                }
                else
                {
                    RemovePhoto();
                }
                if (maxPictures == 5)
                {
                    photoFrame.SetActive(false);

                }
            }
        }
        wincon = inventoryManager2.CalculateTotalValue();
        if (wincon > 2) { questCompleted = true; }



    }

    IEnumerator CapturePhoto()
    {
        if (startSound != null && camsound != null)
        {
            camsound.PlayOneShot(startSound);
        }
        viewingPhoto = true;
        yield return new WaitForEndOfFrame();
        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        Texture2D newScreenshotTexture = new Texture2D((int)regionToRead.width, (int)regionToRead.height, TextureFormat.RGB24, false);
        newScreenshotTexture.ReadPixels(regionToRead, 0, 0, false);
        newScreenshotTexture.Apply();
        float value = 0;
        screenshotOfNPC = IsNPCInScreenshot();
        if (screenshotOfNPC) { value = 1; }
        maxPictures++;
        // Create a unique Sprite instance for this screenshot
        Sprite screenshotSprite = Sprite.Create(newScreenshotTexture, new Rect(0.0f, 0.0f, newScreenshotTexture.width, newScreenshotTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

        ShowPhoto();
        inventoryManager2.ListItems();
        // Add the screenshot as an item to the inventory
        inventoryManager2.AddScreenshotItem(screenshotSprite, "Picture", value);
    }

    public void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;
        photoFrame.SetActive(true);
        StartCoroutine(CameraFlashEfect());
        fadingAnimation.Play("PhotoFade");
    }


    bool IsNPCInScreenshot()
    {
        // Cast a ray from the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, npcLayerMask))
        {
            // Check if the object hit by the ray is tagged as an NPC
            if (hit.collider.CompareTag("NPC"))
            {
                return true; // NPC is in the screenshot
            }
        }

        return false; // NPC is not in the screenshot
    }



    IEnumerator CameraFlashEfect()
    {
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
    }
}

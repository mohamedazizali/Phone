using UnityEngine;
using UnityEngine.UI;

public class BrushSizeButton : MonoBehaviour
{
    public float sizeChangeAmount = 1f; // Amount to change the brush size by

    private DrawMesh drawMesh;

    void Start()
    {
        // Find the DrawMesh script in the scene
        drawMesh = FindObjectOfType<DrawMesh>();

        // Add click event listeners to the buttons
        //transform.Find("IncreaseBrushSizeButton").GetComponent<Button>().onClick.AddListener(IncreaseBrushSize);
        //transform.Find("DecreaseBrushSizeButton").GetComponent<Button>().onClick.AddListener(DecreaseBrushSize);
    }

   public void IncreaseBrushSize()
    {
        // Increase the brush size
        drawMesh.ChangeBrushSize(sizeChangeAmount);
    }

   public void DecreaseBrushSize()
    {
        // Decrease the brush size
        drawMesh.ChangeBrushSize(-sizeChangeAmount); // Pass a negative amount to decrease
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public Color brushColor;

    private DrawMesh drawMesh;

    void Start()
    {
        // Find the DrawMesh script in the scene
        drawMesh = FindObjectOfType<DrawMesh>();

        // Add a click event listener to the button
        GetComponent<Button>().onClick.AddListener(ChangeBrushColor);
    }

    void ChangeBrushColor()
    {
        // Change the brush color to the color assigned to this button
        drawMesh.SetBrushColor(brushColor);
    }
}

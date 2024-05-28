using UnityEngine;
using UnityEngine.SceneManagement;
using DanielLochner.Assets.SimpleScrollSnap;

public class levelselector : MonoBehaviour
{
    public SimpleScrollSnap scrollSnap;

    // Define your scene names here
    public string[] sceneNames = { "level0", "level1", "level2", "level3" };

    public void OnPlayButtonClicked()
    {
        // Get the current selected panel index from SimpleScrollSnap
        int selectedPanelIndex = scrollSnap.SelectedPanel;
        Debug.Log("Selected Panel Index: " + selectedPanelIndex);

        // Load scene based on the selected panel index
        if (selectedPanelIndex >= 0 && selectedPanelIndex < sceneNames.Length)
        {
            SceneManager.LoadScene(sceneNames[selectedPanelIndex]);
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }
        else
        {
            Debug.LogWarning("Invalid panel index: " + selectedPanelIndex);
        }
    }
}
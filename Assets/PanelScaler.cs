using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;

public class PanelScaler : MonoBehaviour
{
    public SimpleScrollSnap scrollSnap;
    public float selectedPanelScale = 1.2f; // Adjust as needed

    private int lastSelectedPanelIndex = -1;

    void Update()
    {
        if (scrollSnap == null)
        {
            Debug.LogWarning("SimpleScrollSnap reference not set!");
            return;
        }

        int currentSelectedPanelIndex = scrollSnap.SelectedPanel;

        if (currentSelectedPanelIndex != lastSelectedPanelIndex)
        {
            // Reset scale for the previously selected panel
            if (lastSelectedPanelIndex >= 0 && lastSelectedPanelIndex < scrollSnap.Panels.Length)
            {
                scrollSnap.Panels[lastSelectedPanelIndex].transform.localScale = Vector3.one;
            }

            // Scale the newly selected panel
            if (currentSelectedPanelIndex >= 0 && currentSelectedPanelIndex < scrollSnap.Panels.Length)
            {
                scrollSnap.Panels[currentSelectedPanelIndex].transform.localScale = Vector3.one * selectedPanelScale;
            }

            lastSelectedPanelIndex = currentSelectedPanelIndex;
        }
    }
}

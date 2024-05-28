using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;

public class currpanel : MonoBehaviour
{
    public SimpleScrollSnap scrollSnap;

    void Start()
    {
        // Assuming you have a reference to the SimpleScrollSnap component
        // Assign it in the inspector
        // Then you can access the CurrentPanel property
        int currentPanelIndex = scrollSnap.SelectedPanel;

        Debug.Log("Current Snapped Panel Index: " + currentPanelIndex);
    }
}
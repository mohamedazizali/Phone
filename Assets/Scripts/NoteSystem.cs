using UnityEngine;


public class NoteSystem : MonoBehaviour
{
    public GameObject noteCanvas; // Reference to the canvas containing the note image
    public KeyCode interactKey = KeyCode.E; // Key to interact with the paper object

    private bool isInRange = false;

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            ShowNote();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            HideNote();
        }
    }

    private void ShowNote()
    {
        noteCanvas.SetActive(true); // Show the note canvas
    }

    private void HideNote()
    {
        noteCanvas.SetActive(false); // Hide the note canvas
    }
}

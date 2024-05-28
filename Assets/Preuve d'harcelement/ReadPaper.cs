using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadPaper : MonoBehaviour
{
    public float interactionDistance = 3f; // Set the interaction distance
    public GameObject interactionPanel; // Reference to the panel in the Canvas

    private GameObject currentTarget;

    void Update()
    {
        CheckForInteraction();
    }

    void CheckForInteraction()
    {
        if (currentTarget != null && Input.GetKeyDown(KeyCode.E))
        {
            interactionPanel.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paper"))
        {
            currentTarget = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Paper"))
        {
            currentTarget = null;
            interactionPanel.SetActive(false);
        }
    }
}

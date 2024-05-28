using UnityEngine;

public class HideableObject : MonoBehaviour
{
    public Transform hidingPosition; // The position inside the hideable object where the player will be placed
    public KeyCode hideKey = KeyCode.E; // The key to trigger hiding
    public GameObject player;
    private bool isPlayerNearby = false;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(hideKey))
        {
            HidePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void HidePlayer()
    {
        // Get the player GameObject
      

        if (player != null)
        {
            // Move the player to the hiding position
            player.transform.position = hidingPosition.position;
            player.transform.rotation = hidingPosition.rotation;
            Debug.Log("Player hidden at: " + hidingPosition.position);
            // Optionally, disable player controls or play animations
        }
    }
}

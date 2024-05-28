using UnityEngine;

public class CameraZoomOnKeypad : MonoBehaviour
{
    public Camera playerCamera; // Reference to the main player camera
    public Camera keypadCamera; // Reference to the keypad camera
    //public GameObject player; // Reference to the player GameObject
    private bool inRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("Player entered keypad range.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            Debug.Log("Player exited keypad range.");
        }
    }
    private void Start()
    {
        keypadCamera.enabled = false;
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!keypadCamera.enabled)
            {
                SwitchToKeypadCamera();
            }
            else
            {
                SwitchToPlayerCamera();
            }
        }
    }

    void SwitchToKeypadCamera()
    {
        // Disable the player GameObject and its camera
        //player.SetActive(false);
        playerCamera.enabled = false;

        // Enable the keypad camera
        keypadCamera.enabled = true;

        // Set cursor state and visibility
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void SwitchToPlayerCamera()
    {
        // Enable the player GameObject and its camera
       // player.SetActive(true);
        playerCamera.enabled = true;

        // Disable the keypad camera
        keypadCamera.enabled = false;

        // Set cursor state and visibility
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
}

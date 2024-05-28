using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    private bool isNearDrawer = false;
    private bool isOpen = false; // Indicates whether the drawer is open or closed
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for player input to interact with the drawer
        if (isNearDrawer && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the isOpen state
            isOpen = !isOpen;

            // Activate the 'clicked' trigger in the animator controller to play the animation
            anim.SetTrigger("clicked");

            // Update the Animator parameter 'isOpen' to reflect the current state
            anim.SetBool("isOpen", isOpen);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDrawer = true;
            // Optionally, display a message to the player indicating they can interact with the drawer
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDrawer = false;
            // Optionally, hide any interaction message displayed to the player
        }
    }
}

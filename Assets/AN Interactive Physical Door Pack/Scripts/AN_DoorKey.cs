using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_DoorKey : MonoBehaviour
{
    [Tooltip("True - red key object, false - blue key")]
    public bool isRedKey = true;
    AN_HeroInteractive hero;

    // NearView()
    float distance;
    float angleView;
    Vector3 direction;
    private bool isNearKey = false;
    private void Start()
    {
        hero = FindObjectOfType<AN_HeroInteractive>(); // key will get up and it will saved in "inventary"
    }

    void Update()
    {
        if ( isNearKey && Input.GetKeyDown(KeyCode.E) )
        {
            if (isRedKey) hero.RedKey = true;
            else hero.BlueKey = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearKey = true;
            Debug.Log("is near lever");
            // Optionally, display a message to the player indicating they can interact with the lever
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearKey = false;
            Debug.Log("is out near level");
            // Optionally, hide any interaction message displayed to the player
        }
    }
}

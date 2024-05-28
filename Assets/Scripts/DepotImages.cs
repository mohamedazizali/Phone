using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepotImages : MonoBehaviour
{
    [SerializeField] QuestManager questManager;
    [SerializeField] ScreenCapture screenCapture;
    private bool inRange = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log("enter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if (inRange && Input.GetKeyUp(KeyCode.E))
        //{
        //    if (screenCapture.questCompleted)
        //    {
        //        questManager.CompleteQuest2();
        //        Debug.Log("images deposited"); }
        //    else
        //    {
        //        Debug.Log("no images of bullies");
        //    }
        //    }
        //}
    }
}


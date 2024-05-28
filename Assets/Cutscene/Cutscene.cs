using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cutscene : MonoBehaviour
{

    public bool cutscene1played = false;
    public GameObject PlayerCam;
    public GameObject cutsceneCam;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player" && cutscene1played == false)
        {
            cutscene1played = true;
            PlayerCam.SetActive(false);
            cutsceneCam.SetActive(true);
            Invoke("SwitchToPlayerCam", 10f);
        }
    }
    void SwitchToPlayerCam()
    {
        PlayerCam.SetActive(true);
        cutsceneCam.SetActive(false);
    }
}

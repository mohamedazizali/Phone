using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject ThirdCam;
    public GameObject FirstCam;
    public int CamMode;
    [SerializeField]
    private ThirdPersonController controller;
    public GameObject InventoryTab1, InventoryTab2, CursorCam;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (InventoryManager.Instance.HasItem("Camera"))
        {
            if (Input.GetButtonDown("Camera"))
            {
                if (CamMode == 1)
                {
                    CursorCam.SetActive(false);
                    CamMode = 0;
                }
                else
                {
                    CamMode = 1;
                    CursorCam.SetActive(true);
                }
                StartCoroutine(CamChange());

            }
        }
        if (Input.GetButtonDown("Inventory")) // Check if the "TAB" button is pressed
        {
            // Toggle the inventoryTab GameObject's active state
            InventoryTab1.SetActive(!InventoryTab1.activeSelf);
            InventoryTab2.SetActive(!InventoryTab2.activeSelf);
            InventoryManager.Instance.ListItems();
            ToggleCursorVisibilityAndLockState();
        }

    }
    void ToggleCursorVisibilityAndLockState()
    {
        // Toggle cursor visibility
        Cursor.visible = !Cursor.visible;

        // Toggle cursor lock state
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);
        if (CamMode == 0)
        {
            ThirdCam.SetActive(true);
            FirstCam.SetActive(false);
            controller.enabled = true;
        }
        if (CamMode == 1)
        {
            ThirdCam.SetActive(false);
            FirstCam.SetActive(true);
            controller.enabled = false;
        }
    }
}

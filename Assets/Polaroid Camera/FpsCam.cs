using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCam : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public float zoomSpeed = 100f;
    public float zoomMinFOV = 20f;
    public float zoomMaxFOV = 60f;

    private float xRotation = 0f;
    private Camera cam;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        // Zooming
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float newFOV = cam.fieldOfView - scroll * zoomSpeed;
            newFOV = Mathf.Clamp(newFOV, zoomMinFOV, zoomMaxFOV);
            cam.fieldOfView = newFOV;
        }
    }
}

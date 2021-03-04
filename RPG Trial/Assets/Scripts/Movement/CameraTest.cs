using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public float mouseSensivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
   public  Vector3 offset = new Vector3(0, 1, -3);

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.position =playerBody.position + offset;
    }

    private void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

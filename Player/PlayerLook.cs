using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;

    [SerializeField] private WallRun wallRun;

    [SerializeField] Transform playerCamera;

    float mouseX;
    float mouseY;

    float multiplier = 5f;

    float xRotation;
    float yRotation;

    public bool isCursorLocked = true;

    private void Start()
    {
        //Locks the cursor and hides it
        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        MyInput();

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, wallRun.tilt); //Rotates camera on wallrun
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensitivityX * multiplier;
        xRotation -= mouseY * sensitivityY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //clamp to straight up and down
    }
}

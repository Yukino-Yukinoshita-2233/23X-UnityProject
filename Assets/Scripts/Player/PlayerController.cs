using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Game.Constants;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private float moveSpeed = 3f;
    private float mouseX;
    private float mouseY;
    private CharacterController controller;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        MouseController();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis(GameConstants.AxisNameHorizontal);
        float vertical = Input.GetAxis(GameConstants.AxisNameVertical);
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        if (dir != Vector3.zero)
        {
            controller.Move(transform.rotation * dir * moveSpeed * Time.deltaTime);
        }


    }

    void MouseController()
    {
        mouseX = Input.GetAxis(GameConstants.MouseAxisNameHorizontal) * rotationSpeed * 10 * Time.deltaTime;
        transform.Rotate(new Vector3(0f, mouseX, 0f), Space.Self);

        mouseY -= Input.GetAxis(GameConstants.MouseAxisNameVertical) * rotationSpeed * 10 * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -40f, 40f);
        Camera.main.transform.localEulerAngles = new Vector3(mouseY, 0, 0);
    }
}

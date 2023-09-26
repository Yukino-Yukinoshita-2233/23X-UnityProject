using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Game.Constants;
using System;
using Unity.VisualScripting;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float moveSpeed = 3f;
    private float mouseX;
    private float mouseY;
    private CharacterController controller;
    public float forceMagnitude = 10f; // 力的大小
    private Rigidbody rb; // 物体的刚体组件

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        MouseController();
        Gravity();

    }

    private void Gravity()
    {
        // 获取物体的位置
        Vector3 objectPosition = transform.position;

        // 从物体中心向下发射一条射线
        Ray ray = new Ray(objectPosition, -Vector3.up);

        // 创建一个 RaycastHit 对象，用于存储射线检测的结果
        RaycastHit hit;

        

        // 进行射线检测
        if (Physics.Raycast(ray, out hit))
        {
            // 检测到物体
            GameObject hitObject = hit.collider.gameObject;
            float distance = hit.distance; // 获取射线击中物体的距离

            Debug.Log("射线击中了物体：" + hitObject.name);
            Debug.Log("距离：" + distance);
            if (distance > 1)
            {
                gameObject.transform.position -= new Vector3(0, forceMagnitude * Time.deltaTime, 0);
            }

        }
        else
        {
            gameObject.transform.position -= new Vector3(0, forceMagnitude * Time.deltaTime, 0);
        }
        


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

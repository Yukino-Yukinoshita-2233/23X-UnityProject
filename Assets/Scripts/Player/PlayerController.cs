using UnityEngine;
using Unity.Game.Constants;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float moveSpeed = 3f;
    private float mouseX;
    private float mouseY;
    private CharacterController controller;
    public float forceMagnitude = 10f; // ���Ĵ�С
    private Rigidbody rb; // ����ĸ������

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
        // ��ȡ�����λ��
        Vector3 objectPosition = transform.position;

        // �������������·���һ������
        Ray ray = new Ray(objectPosition, -Vector3.up);

        // ����һ�� RaycastHit �������ڴ洢���߼��Ľ��
        RaycastHit hit;



        // �������߼��
        if (Physics.Raycast(ray, out hit))
        {
            // ��⵽����
            GameObject hitObject = hit.collider.gameObject;
            float distance = hit.distance; // ��ȡ���߻�������ľ���

            //Debug.Log("���߻��������壺" + hitObject.name);
            //Debug.Log("���룺" + distance);
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

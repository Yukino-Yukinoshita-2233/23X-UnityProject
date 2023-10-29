using UnityEngine;
using Unity.Game.Constants;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float moveSpeed = 3f;
    private float mouseX;
    private float mouseY;
    private CharacterController controller;
    public float forceMagnitude = 10f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        MouseController();
        Gravity();
    }

    private void Gravity()
    {
        Vector3 objectPosition = transform.position;
        Ray ray = new Ray(objectPosition, -Vector3.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            float distance = hit.distance;

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
    }

}

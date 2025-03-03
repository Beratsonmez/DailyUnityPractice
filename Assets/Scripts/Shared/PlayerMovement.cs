using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] private float SensX;
    [SerializeField] private float SensY;
    [SerializeField] private float speed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform playerCamera;
    private Vector3 velocity;

    private float xRotation = 0f;


    private void Start()
    {
        CharacterController controller = GetComponent<CharacterController>();
        speed = walkSpeed;
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
        MovePlayer();
        RotateCamera();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * Time.deltaTime * speed);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            speed = runSpeed;
        }
        else
        {
            isRunning = false;
            speed = walkSpeed;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void RotateCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        float mouseX = Input.GetAxis("Mouse X") * SensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * SensY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    

}

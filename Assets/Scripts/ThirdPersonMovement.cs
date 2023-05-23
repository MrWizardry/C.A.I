using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float sensivity = 1f;
    [SerializeField] private float cameraSensitivity = 3f;

    private float turnSmoothVelocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            RotatePlayer(direction);
            MovePlayer(direction);
            animator.SetBool("IsWalking", true);
        }
        else
            animator.SetBool("IsWalking", false);

        if (Input.GetKeyDown(KeyCode.G))
            Application.Quit();
    }

    private void RotatePlayer(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity;

        cam.Rotate(Vector3.up, mouseX, Space.World);
        cam.Rotate(Vector3.left, mouseY, Space.Self);
    }

    private void MovePlayer(Vector3 direction)
    {
        Vector3 moveDir = Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * Vector3.forward;
        controller.Move(moveDir.normalized * (movementSpeed * Time.deltaTime * sensivity));
    }

    
}

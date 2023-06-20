using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public Slider staminaSlider;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float cameraSensitivity = 3f;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaConsumptionRate = 10f;
    [SerializeField] private float staminaRegenerationRate = 5f;

    private float turnSmoothVelocity;
    private bool isRunning = false;
    private float currentStamina;

    private void Awake()
    {
        Cursor.visible = false;
        currentStamina = maxStamina;

        UpdateStaminaSlider();
    }

    private void Update()
    {
        HandleInput();
        UpdateStamina();
        UpdateStaminaSlider();
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
        {
            animator.SetBool("IsWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentStamina > 0)
        {
            isRunning = true;
            animator.SetBool("IsRunning", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || currentStamina <= 0)
        {
            isRunning = false;
            Debug.Log("No Energy");
            animator.SetBool("IsRunning", false);
        }

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
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 moveDir = Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * direction;
        controller.Move(moveDir.normalized * (currentSpeed * Time.deltaTime * sensitivity));
    }

    private void UpdateStamina()
    {
        if (isRunning && currentStamina > 0)
        {
            currentStamina -= staminaConsumptionRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
        else if (!isRunning && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenerationRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
    }

    private void UpdateStaminaSlider()
    {
        float normalizedStamina = currentStamina / maxStamina;
        staminaSlider.value = normalizedStamina;
    }


}

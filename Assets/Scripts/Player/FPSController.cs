using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class FPSControllerRigidbody : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float runSpeed = 9f;
    public float crouchSpeed = 2.5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;

    [Header("Stamina")]
    public float maxStamina = 5f;
    public float staminaDrainRate = 1f;
    public float staminaRegenRate = 0.5f;

    [Header("Crouch")]
    public float standingHeight = 2f;
    public float crouchHeight = 1f;

    [Header("Aim / Zoom")]
    public float normalFOV = 60f;
    public float aimFOV = 30f;
    public float zoomSpeed = 10f;

    [Header("References")]
    public Gun gun;
    public Transform playerCamera;

    private Rigidbody rb;
    private CapsuleCollider col;
    private Camera cam;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private float xRotation = 0f;
    private bool jumpRequest = false;

    private bool isCrouching = false;
    private bool isAiming = false;
    private bool isRunning = false;

    public float currentStamina;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        col = GetComponent<CapsuleCollider>();
        cam = playerCamera.GetComponent<Camera>();

        cam.fieldOfView = normalFOV;

        currentStamina = maxStamina;
    }

    void Update()
    {
        // Look rotation
        float mouseX = lookInput.x * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * 100f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Smooth zoom when aiming
        float targetFOV = isAiming ? aimFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);

        // Handle stamina drain/regeneration
        HandleStamina();

        // Stop sprint if player stops moving
        if (moveInput.magnitude < 0.1f)
        {
            isRunning = false;
        }
    }

    void FixedUpdate()
    {
        // Movement direction
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        float currentSpeed = speed;

        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else if (isRunning && currentStamina > 0)
        {
            currentSpeed = runSpeed;
        }

        Vector3 targetVelocity = move * currentSpeed;

        Vector3 velocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        rb.linearVelocity = velocity;

        // Jump
        if (jumpRequest && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }

        jumpRequest = false;
    }

    void HandleStamina()
    {
        if (isRunning && moveInput.magnitude > 0.1f && !isCrouching)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;

            if (currentStamina <= 0)
            {
                currentStamina = 0;
                isRunning = false;
            }
        }
        else
        {
            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    // INPUT FUNCTIONS

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void OnJump()
    {
        jumpRequest = true;
    }

    public void OnShoot()
    {
        gun.Shoot();
        Debug.Log(gameObject.name + " fired");
    }

    // Crouch (East Button)
    public void OnCrouch()
    {
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            col.height = crouchHeight;
            isRunning = false;
        }
        else
        {
            col.height = standingHeight;
        }
    }

    // Sprint (Press Left Stick / L3)
    public void OnSprint()
    {
        if (!isCrouching && !isAiming && currentStamina > 0 && moveInput.magnitude > 0.1f)
        {
            isRunning = true;
        }
    }

    // Aim / Zoom (Left Trigger)
    public void OnAim(InputValue value)
    {
        float triggerValue = value.Get<float>();
        isAiming = triggerValue > 0.1f;
    }

    // Reload (West Button)
    public void OnReload()
    {
        gun.Reload();
    }
}
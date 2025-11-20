using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class MobileInputManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cameraTransform;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 velocity;
    private bool isGrounded;
    private Vector2 moveInput;
    private Vector2 lookInput;

    // Ограничения вращения камеры
    private float xRotation = 0f;
    [SerializeField] private float minXAngle = -80f;
    [SerializeField] private float maxXAngle = 80f;

    void Start()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        EnhancedTouchSupport.Enable();

        // Настройка для мобилки
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        HandleTouchInput();
        HandleMovement();
        HandleCameraLook();
    }

    void HandleTouchInput()
    {
        moveInput = Vector2.zero;
        lookInput = Vector2.zero;

        foreach (var touch in Touch.activeTouches)
        {
            // Левая треть экрана - движение
            if (touch.screenPosition.x < Screen.width * 0.3f)
            {
                if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
                {
                    moveInput = touch.delta.normalized;
                }
            }
            // Правая часть - камера
            else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                lookInput = touch.delta * lookSensitivity * 0.01f;
            }
        }
    }

    void HandleMovement()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        // Гравитация
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void HandleCameraLook()
    {
        if (lookInput != Vector2.zero)
        {
            // Горизонтальное вращение персонажа
            transform.Rotate(Vector3.up * lookInput.x);

            // Вертикальное вращение камеры
            xRotation -= lookInput.y;
            xRotation = Mathf.Clamp(xRotation, minXAngle, maxXAngle);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    // Методы для UI кнопок
    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    public void Shoot()
    {
        PlayerEvents.PlayerShoted();
    }
}
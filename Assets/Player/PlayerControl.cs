using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private Joystick moveJoystick;

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private bool canJump = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            characterController = gameObject.AddComponent<CharacterController>();
        }
    }

    void Update()
    {
        MovePlayer();

        
        if (characterController.isGrounded && !isGrounded)
        {
            canJump = true;
        }
        isGrounded = characterController.isGrounded;
    }

    void MovePlayer()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector2 joystickInput = new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical);

        if (joystickInput.magnitude > 0.1f) // мертвая зона
        {
            Vector3 move = transform.right * joystickInput.x + transform.forward * joystickInput.y;
            characterController.Move(move * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (canJump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            canJump = false; // Блокируем прыжок до приземления
        }
    }

    public void Shoot()
    {
        PlayerEvents.PlayerShoted();
    }
}
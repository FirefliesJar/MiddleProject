using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private Rigidbody2D rb;

    private Vector2 moveVector;
    private bool jumpRequested = false;
    private bool isGrounded = false;

    private InputAction moveAction;
    private InputAction jumpAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        jumpAction.performed += Jump;
    }

    private void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = moveVector.x * movementSpeed;

        if (jumpRequested)
        {
            rb.linearVelocityY = jumpForce;
            jumpRequested = false;
            isGrounded = false;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
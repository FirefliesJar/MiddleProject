using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f, jumpForce = 5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject visuals;
    private Vector2 moveVector;
    private bool jumpRequested = false;

    private InputAction moveAction, jumpAction;

    private bool isGrounded = false;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        jumpAction.performed += Jump;   //+= means subscribing to an event - when it's called through the input system it calles the Jump function
    }

    void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        
        if (moveVector.x > 0)   //change visuals direction based on movement direction
        {
            visuals.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveVector.x < 0)
        {
            visuals.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = moveVector.x * movementSpeed;

        if (jumpRequested)
        {
            //later we can add here jump sound
            rb.linearVelocityY = jumpForce;
            jumpRequested = false;
        }
    }

    void Jump(InputAction.CallbackContext context)
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
            Vector2 contactNormal = collision.contacts[0].normal;
            if (contactNormal.y > 0.5f)
            {
                isGrounded = true;
            }
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
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f, jumpForce = 5f;

    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveVector;
    private bool jumpRequested = false;

    private InputAction moveAction, jumpAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        jumpAction.performed += Jump;  //+= means subscribing to an event - when it's called through the input system it calles the Jump function
    }

    void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();
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
        jumpRequested = true;
    }
}
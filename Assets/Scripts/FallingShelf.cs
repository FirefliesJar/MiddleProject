using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private CharacterSwitch characterSwitch; 
    
    public float shakeTime = 0.5f;
    public float fallTime = 0.7f;
    public float respawnWait = 3f;

    public float shakeAmount = 0.05f;
    public float shakeSpeed = 30f;

    private bool isFalling;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D platformCollider;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        platformCollider = GetComponent<Collider2D>();

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isFalling && collision.gameObject.CompareTag("Player") && characterSwitch.currentCharacterIndex != 1)
        {
            // Only activate when player lands on top
            if (collision.contacts[0].normal.y < -0.5f)
            {
                StartCoroutine(FallAndRespawn());
            }
        }
    }

    private IEnumerator FallAndRespawn()
    {
        isFalling = true;
        this.tag = "Untagged"; // Prevents further collisions while falling

        // SHAKE
        float timer = 0f;

        while (timer < shakeTime)
        {
            float shake = Mathf.Sin(timer * shakeSpeed) * shakeAmount;

            transform.position =
                startPosition + new Vector3(shake, 0f, 0f);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;

        // START FALLING
        // Collider stays ON, so the player falls with the platform
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.freezeRotation = true;

        // Let the platform fall for a moment
        yield return new WaitForSeconds(fallTime);

        // NOW make it disappear
        spriteRenderer.enabled = false;
        platformCollider.enabled = false;

        // Stop the platform
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Wait before respawn
        yield return new WaitForSeconds(respawnWait);

        // Put it back
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Make it static again
        rb.bodyType = RigidbodyType2D.Static;

        spriteRenderer.enabled = true;
        platformCollider.enabled = true;

        isFalling = false;
        this.tag = "Ground"; // Reset tag to allow collisions again
    }
}
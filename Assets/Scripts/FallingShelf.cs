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
        if (!isFalling &&
            collision.gameObject.CompareTag("Player") &&
            characterSwitch.currentCharacterIndex != 1)
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
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.freezeRotation = true;

        // Let the platform fall
        yield return new WaitForSeconds(fallTime);

        // Move the platform away so Player gets a normal collision exit
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y - 10f,
            transform.position.z
        );

        // Wait for Unity's next physics update
        yield return new WaitForFixedUpdate();

        // NOW disable the platform
        platformCollider.enabled = false;
        spriteRenderer.enabled = false;

        // Stop movement
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Wait before respawning
        yield return new WaitForSeconds(respawnWait);

        // Return platform to its original position
        transform.position = startPosition;
        transform.rotation = startRotation;

        rb.bodyType = RigidbodyType2D.Static;

        platformCollider.enabled = true;
        spriteRenderer.enabled = true;

        isFalling = false;
    }
}

using UnityEngine;

public class WitchChase : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
    }

    private void Update()
    {
        if (player == null)
            return;

        if (player.position.x > transform.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && player.GetComponent<CandyCollection>().Candy < 3)
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage();

                // Return witch to starting position
                transform.position = startPosition;
            }
        }
    }
}

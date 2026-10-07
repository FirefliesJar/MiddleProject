using UnityEngine;

public class WindZone : MonoBehaviour
{
    public float windForce = 10f;
    public Vector2 windDirection = Vector2.right;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                playerRb.AddForce(
                    windDirection.normalized * windForce,
                    ForceMode2D.Force
                );
            }
        }
    }
}
using UnityEngine;

public class WindPushZone : MonoBehaviour
{
    [SerializeField] private float windForce = 5f;
    [SerializeField] private Vector2 windDirection = Vector2.left;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddForce(
                    windDirection.normalized * windForce,
                    ForceMode2D.Force
                );
            }
        }
    }
}
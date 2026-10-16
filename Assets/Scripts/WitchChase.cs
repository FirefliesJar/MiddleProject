using UnityEngine;

public class WitchChase : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public GameObject loseScreen;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // מוצא את ה-Sprite Renderer של המכשפה
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null)
            return;

        // בודק איפה השחקן ביחס למכשפה
        if (player.position.x > transform.position.x)
        {
            // השחקן מימין - המכשפה מסתכלת ימינה
            spriteRenderer.flipX = true;
        }
        else
        {
            // השחקן משמאל - המכשפה מסתכלת שמאלה
            spriteRenderer.flipX = false;
        }

        // מזיז את המכשפה לכיוון השחקן
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // אם המכשפה נגעה בשחקן
        if (other.CompareTag("Player"))
        {
            Debug.Log("YOU LOST!");

            // מציג את מסך ההפסד
            if (loseScreen != null)
            {
                loseScreen.SetActive(true);
            }

            // עוצר את המשחק
            Time.timeScale = 0f;
        }
    }
}
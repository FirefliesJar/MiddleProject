
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private ButtonsMenu buttonsMenu;
    
    public int maxHealth = 3;
    public float blinkDuration = 1.5f;
    public float blinkInterval = 0.15f;

    private int currentHealth;
    private bool isInvincible = false;

    private SpriteRenderer[] sprites;
    private AudioSource hitSound;

    private void Start()
    {
        currentHealth = maxHealth;

        sprites = GetComponentsInChildren<SpriteRenderer>(true);
        hitSound = GetComponent<AudioSource>();
    }

    public void TakeDamage()
    {
        // Prevent damage while blinking or after losing all lives
        if (isInvincible || currentHealth <= 0)
            return;

        // Lose one life
        currentHealth--;

        Debug.Log("Lives left: " + currentHealth);

        // Play hit sound
        if (hitSound != null)
        {
            hitSound.Play();
        }

        // Restart the level after 3 hits
        if (currentHealth <= 0)
        {
            Time.timeScale = 1f;

            buttonsMenu.RestartScene();
        }
        else
        {
            // Blink and become temporarily invincible
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        isInvincible = true;

        float timer = 0f;

        while (timer < blinkDuration)
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.enabled = false;
            }

            yield return new WaitForSeconds(blinkInterval);

            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.enabled = true;
            }

            yield return new WaitForSeconds(blinkInterval);

            timer += blinkInterval * 2f;
        }

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = true;
        }

        isInvincible = false;
    }
}

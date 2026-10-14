using System.Collections;
using UnityEngine;

public class LockFall : MonoBehaviour
{
    public float shakeTime = 0.7f;
    public float shakeAmount = 0.08f;
    public float shakeSpeed = 30f;

    public Portal portal;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Vector3 startPosition;
    private bool hasFallen = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        startPosition = transform.position;

        // Lock stays in place at the beginning
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
    }

    public void DropLock()
    {
        if (!hasFallen)
        {
            StartCoroutine(ShakeAndFall());
        }
    }

    private IEnumerator ShakeAndFall()
    {
        hasFallen = true;

        float timer = 0f;

        // Shake the lock
        while (timer < shakeTime)
        {
            float shake = Mathf.Sin(timer * shakeSpeed) * shakeAmount;

            transform.position =
                startPosition + new Vector3(shake, 0f, 0f);

            timer += Time.deltaTime;

            yield return null;
        }

        // Return to center
        transform.position = startPosition;

        // Play lock sound
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Make the lock fall
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 3f;

        // Unlock the portal
        if (portal != null)
        {
            portal.UnlockPortal();
        }
    }
}
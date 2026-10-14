using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private bool portalUnlocked = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && portalUnlocked)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    public void UnlockPortal()
    {
        portalUnlocked = true;
        Debug.Log("Portal unlocked!");
    }
}
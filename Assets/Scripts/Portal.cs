using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private ButtonsMenu buttonsMenu;
    private bool portalUnlocked = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && portalUnlocked)
        {
            buttonsMenu.NextScene();
        }
    }

    public void UnlockPortal()
    {
        portalUnlocked = true;
        Debug.Log("Portal unlocked!");
    }
}
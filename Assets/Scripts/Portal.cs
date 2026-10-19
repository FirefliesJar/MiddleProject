using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private ButtonsMenu buttonsMenu;
    [SerializeField] private GameObject portalPlatform;
    [SerializeField] private GameObject witch;

    private bool portalUnlocked = false;

    void Start()
    {
        portalPlatform.GetComponent<PlatformEffector2D>().enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && portalUnlocked)
        {
            witch.GetComponent<WitchChase>().endChase = true;
            buttonsMenu.NextScene();
        }
    }

    public void UnlockPortal()
    {
        portalUnlocked = true;
        portalPlatform.GetComponent<PlatformEffector2D>().enabled = true;
        Debug.Log("Portal unlocked!");
    }
}
using UnityEngine;

public class Instructions : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0f;
    }

    public void StartGameButton()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}

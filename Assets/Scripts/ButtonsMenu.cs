using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMenu : MonoBehaviour
{
    [SerializeField] private SceneFade sceneFade;
    
    public void NextScene()
    {
        Time.timeScale = 1f;

        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % sceneCount;

        sceneFade.LoadFadedScene(nextSceneIndex);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
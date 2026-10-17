using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private float fadeToBlackDuration = 0.5f;
    [SerializeField] private float fadeFromBlackDuration = 1.2f;

    private Image blackImage;

    private void Awake()
    {
        blackImage = GetComponent<Image>();
        SetAlpha(1f);
    }

    private IEnumerator Start()
    {
        yield return null;
        yield return FadeTo(0f, fadeFromBlackDuration);
    }

    public void LoadFadedScene(int sceneBuildIndex)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutAndLoad(sceneBuildIndex));
    }

    private IEnumerator FadeOutAndLoad(int sceneBuildIndex)
    {
        yield return FadeTo(1f, fadeToBlackDuration);
        SceneManager.LoadScene(sceneBuildIndex);
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startingAlpha = blackImage.color.a;
        float startTime = Time.unscaledTime;

        while (Time.unscaledTime - startTime < duration)
        {
            float timePassed = Time.unscaledTime - startTime;
            float progress = Mathf.Clamp01(timePassed / duration);

            SetAlpha(Mathf.Lerp(startingAlpha, targetAlpha, progress));
            yield return null;
        }

        SetAlpha(targetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        Color color = blackImage.color;
        color.a = alpha;
        blackImage.color = color;
        }
}
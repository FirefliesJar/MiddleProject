using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;

    private Image blackImage;

    private void Awake()
    {
        blackImage = GetComponent<Image>();
        SetAlpha(1f);
    }

    private IEnumerator Start()
    {
        yield return FadeTo(0f);
    }

    public void LoadFadedScene(int sceneBuildIndex)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutAndLoad(sceneBuildIndex));
    }

    private IEnumerator FadeOutAndLoad(int sceneBuildIndex)
    {
        yield return FadeTo(1f);
        SceneManager.LoadScene(sceneBuildIndex);
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startingAlpha = blackImage.color.a;
        float timePassed = 0f;

        while (timePassed < fadeDuration)
        {
            timePassed += Time.unscaledDeltaTime;
            float progress = timePassed / fadeDuration;

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
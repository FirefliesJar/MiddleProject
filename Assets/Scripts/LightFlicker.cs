
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private Light2D candleLight;

    [SerializeField] private float minIntensity = 0.8f;
    [SerializeField] private float maxIntensity = 1.2f;
    [SerializeField] private float flickerSpeed = 2f;

    private float randomOffset;

    private void Start()
    {
        randomOffset = Random.Range(0f, 100f);
    }

    private void Update()
    {
        float noise = Mathf.PerlinNoise(
            Time.time * flickerSpeed + randomOffset, 0f
        );

        candleLight.intensity = Mathf.Lerp(
            minIntensity, maxIntensity, noise
        );
    }
}

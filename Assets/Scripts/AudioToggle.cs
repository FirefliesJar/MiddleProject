using UnityEngine;

public class AudioToggle : MonoBehaviour
{
    public void ToggleAudio()
    {
        AudioListener.volume = AudioListener.volume > 0f ? 0f : 1f;
    }
}
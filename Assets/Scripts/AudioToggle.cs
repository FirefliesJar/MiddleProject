using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    [SerializeField] private Image audioIcon;
    [SerializeField] private Sprite audioOnSprite;
    [SerializeField] private Sprite audioOffSprite;
    
    void Start()
    {
        // Set the initial icon based on the current audio state
        audioIcon.sprite = AudioListener.volume > 0f ? audioOnSprite : audioOffSprite;
    }
    
    public void ToggleAudio()
    {
        AudioListener.volume = AudioListener.volume > 0f ? 0f : 1f;
        audioIcon.sprite = AudioListener.volume > 0f ? audioOnSprite : audioOffSprite;
    }
}
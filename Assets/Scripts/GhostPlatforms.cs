using UnityEngine;

public sealed class GhostPlatforms : MonoBehaviour
{
    [SerializeField] private CharacterSwitch characterSwitch;
    private Collider2D platformCollider;
    private SpriteRenderer platformRenderer;
    [SerializeField] private Color ghostColor;

    void Awake()
    {
        characterSwitch.OnCharacterSwitched += UpdateGhostPlatforms;
        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        platformCollider.enabled = characterSwitch.currentCharacterIndex == 1; // Enable if Ghost is active
        UpdateGhostPlatforms(characterSwitch.currentCharacterIndex);
    }

    private void UpdateGhostPlatforms(int characterIndex)
    {
        platformCollider.enabled = characterIndex == 1; // Enable if Ghost is active
        platformRenderer.color = characterIndex == 1 ? Color.white : ghostColor; // Change color
    }

}
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CharacterSwitch : MonoBehaviour
{
    private InputAction switchAction;

    [SerializeField]
    private List<GameObject> characterOptions = new List<GameObject>();

    // 0 = Doggo, 1 = Ghost, 2 = Pumpkin
    public int currentCharacterIndex = 0;

    [SerializeField]
    private ParticleSystem switchEffect;

    private int playerLayer;
    private int ghostPlatformLayer;

    private void Awake()
    {
        switchAction = InputSystem.actions.FindAction("SwitchCharacter");
        switchAction.performed += SwitchCharacter;

        playerLayer = gameObject.layer;
        ghostPlatformLayer = LayerMask.NameToLayer("GhostPlatform");
    }

    private void Start()
    {
        foreach (GameObject character in characterOptions)
        {
            character.SetActive(false);
        }

        characterOptions[currentCharacterIndex].SetActive(true);

        UpdateGhostPlatforms();
    }

    private void SwitchCharacter(InputAction.CallbackContext context)
    {
        // Turn off current character
        characterOptions[currentCharacterIndex].SetActive(false);

        // Move to next character
        currentCharacterIndex =
            (currentCharacterIndex + 1) % characterOptions.Count;

        // Turn on new character
        characterOptions[currentCharacterIndex].SetActive(true);

        if (switchEffect != null)
        {
            switchEffect.Play();
        }

        Debug.Log(
            "Switched to character: " +
            characterOptions[currentCharacterIndex].name
        );

        UpdateGhostPlatforms();
    }

  private void UpdateGhostPlatforms()
{
    // Index 1 = Ghost
    bool isGhost = (currentCharacterIndex == 1);

    // Find every Collider2D in the scene
    Collider2D[] allColliders = FindObjectsByType<Collider2D>(
        FindObjectsSortMode.None
    );

    foreach (Collider2D col in allColliders)
    {
        // Check if this object is on the GhostPlatform layer
        if (col.gameObject.layer == LayerMask.NameToLayer("GhostPlatform"))
        {
            // Collider is active ONLY when the character is Ghost
            col.enabled = isGhost;
        }
    }
}

    private void OnDestroy()
    {
        switchAction.performed -= SwitchCharacter;
    }
}
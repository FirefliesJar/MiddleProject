using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CharacterSwitch : MonoBehaviour
{
    private InputAction switchAction;
    [SerializeField] private List<GameObject> characterOptions = new List<GameObject>(); // List of characters to switch between
    public int currentCharacterIndex = 0; // 0 = Doggo, 1 = Ghost, 2 = Pumpkin

    [SerializeField] private ParticleSystem switchEffect; // Particle effect to play when switching characters

    private void Awake()
    {
        switchAction = InputSystem.actions.FindAction("SwitchCharacter");
        switchAction.performed += SwitchCharacter;
    }

    private void Start()
    {
        foreach (GameObject character in characterOptions)
        {
            character.SetActive(false);
        }
        characterOptions[currentCharacterIndex].SetActive(true); 
    }

    private void SwitchCharacter(InputAction.CallbackContext context)
    {
        characterOptions[currentCharacterIndex].SetActive(false); // Deactivate current character
        currentCharacterIndex = (currentCharacterIndex + 1) % characterOptions.Count; // Cycle through character options
        characterOptions[currentCharacterIndex].SetActive(true); // Activate new character
        if (switchEffect != null)
        {
            switchEffect.Play();
        }
        Debug.Log("Switched to character: " + characterOptions[currentCharacterIndex].name);
    }
}
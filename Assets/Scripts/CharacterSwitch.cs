using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CharacterSwitch : MonoBehaviour
{
    private InputAction switchAction;
    [SerializeField] private List<GameObject> characterOptions; // List of characters to switch between
    private int currentCharacterIndex = 0; // 0 = Doggo, 1 = Ghost, 2 = Pumpkin

    private void Awake()
    {
        switchAction = InputSystem.actions.FindAction("SwitchCharacter");
        switchAction.performed += SwitchCharacter;
    }

    private void SwitchCharacter(InputAction.CallbackContext context)
    {
        characterOptions[currentCharacterIndex].SetActive(false); // Deactivate current character
        currentCharacterIndex = (currentCharacterIndex + 1) % characterOptions.Count; // Cycle through character options
        characterOptions[currentCharacterIndex].SetActive(true); // Activate new character
        Debug.Log("Switched to character: " + characterOptions[currentCharacterIndex].name);
    }
}
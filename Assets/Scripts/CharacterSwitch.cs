using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class CharacterSwitch : MonoBehaviour
{
    private InputAction switchAction;

    [SerializeField] private List<GameObject> characterOptions = new List<GameObject>();
    public int currentCharacterIndex = 0;    // 0 = Doggo, 1 = Ghost, 2 = Pumpkin

    [SerializeField] private ParticleSystem switchEffect;

    public event Action<int> OnCharacterSwitched;

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
        characterOptions[currentCharacterIndex].SetActive(false); //turn off current character
        currentCharacterIndex = (currentCharacterIndex + 1) % characterOptions.Count; //next character
        characterOptions[currentCharacterIndex].SetActive(true); //turn on new character

        if (switchEffect != null)
        {
            switchEffect.Play();
        }

        Debug.Log("Switched to character: " + characterOptions[currentCharacterIndex].name);

        OnCharacterSwitched?.Invoke(currentCharacterIndex);
    }

    private void OnDestroy()
    {
        switchAction.performed -= SwitchCharacter;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalControlManager : MonoBehaviour
{
    #region Declarations
    public enum Animal
    {
        Rooster,
        Cat,
        Dog,
        Donkey,
    };

    public CharacterBase[] characters = new CharacterBase[4];
    public CharacterBase ActiveAnimal;
    public Animal ActiveAnimalIndex = 0;

    private PlayerInput inputActions;
    #endregion

    private void Awake()
    {
        inputActions = new PlayerInput();
    }

    private void Start()
    {
        ActiveAnimal = characters[(int)ActiveAnimalIndex];
        ActiveAnimal.toggleControl();
    }

    private void Update()
    {
        ActiveAnimal.Move(inputActions.Character.Move.ReadValue<Vector2>());
    }

    private void changeCharacters()
    {
        ActiveAnimal.toggleControl();
        ActiveAnimalIndex = (Animal)(((int)ActiveAnimalIndex + 1) % characters.Length);
        ActiveAnimal = characters[(int)ActiveAnimalIndex];
        ActiveAnimal.toggleControl();
    }

    private void Jump()
    {
        ActiveAnimal.Jump();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Character.SwitchCharacter.performed += ctx => changeCharacters();
        inputActions.Character.Jump.performed += ctx => Jump();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Character.SwitchCharacter.performed -= ctx => changeCharacters();
        inputActions.Character.Jump.performed -= ctx => Jump();
    }
}

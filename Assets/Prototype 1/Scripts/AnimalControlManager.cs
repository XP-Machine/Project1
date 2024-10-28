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
        
        //Initialize StackManager
      //  StackManager.Initialize();
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

    public void ChangeCharacters()
    {
        ActiveAnimal.toggleControl();
        ActiveAnimalIndex = (Animal)(((int)ActiveAnimalIndex + 1) % characters.Length);
        ActiveAnimal = characters[(int)ActiveAnimalIndex];
        ActiveAnimal.toggleControl();
    }
    public void ChangeCharacters(Animal animal)
    {
        ActiveAnimal.toggleControl();
        ActiveAnimalIndex = animal;
        ActiveAnimal = characters[(int)ActiveAnimalIndex];
        ActiveAnimal.toggleControl();
    }

    private void Jump()
    {
        ActiveAnimal.Jump();
    }
    private void Ability1()
    {
        ActiveAnimal.AbilityX();
    }
    private void Ability2()
    {
        ActiveAnimal.AbilityY();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Character.SwitchCharacter.performed += ctx => ChangeCharacters();
        inputActions.Character.Jump.performed += ctx => Jump();
        inputActions.Character.Ability1.performed += ctx => Ability1();
        inputActions.Character.Ability2.performed += ctx => Ability2();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Character.SwitchCharacter.performed -= ctx => ChangeCharacters();
        inputActions.Character.Jump.performed -= ctx => Jump();
        inputActions.Character.Ability1.performed -= ctx => Ability1();
        inputActions.Character.Ability2.performed -= ctx => Ability2();
    }
}

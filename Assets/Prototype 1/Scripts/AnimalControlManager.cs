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
    }
    private void Update()
    {
      //  Vector3 movement = inputActions.Character.Move.ReadValue<Vector2>();
      //  movement = new Vector3(movement.x,0,movement.z);
      //  print(movement);
        ActiveAnimal.Move(inputActions.Character.Move.ReadValue<Vector2>());
    }

    private void changeCharacters()
    {
        ActiveAnimalIndex = (Animal)(((int)ActiveAnimalIndex + 1) % 4);
        ActiveAnimal = characters[(int)ActiveAnimalIndex];
    }
    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Character.SwitchCharacter.performed += ctx => changeCharacters();
    }
    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Character.SwitchCharacter.performed -= ctx => changeCharacters();
    }
}

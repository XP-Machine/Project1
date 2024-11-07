using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager_animals : MonoBehaviour
{
    #region Declarations
    public enum Animal
    {
        Rooster,
        Cat,
        Dog,
        Donkey, 
    };

    public animal_base[] characters = new animal_base[4];
    public animal_base ActiveAnimal;
    public float cameraDistance = 10f;
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        ActiveAnimal = characters[(int)ActiveAnimalIndex];
        ActiveAnimal.toggleControl();
        
        SetupCamera();
    }

    private void Update()
    {
        ActiveAnimal.Move(inputActions.Character.Move.ReadValue<Vector2>());
        UpdateCamera();
    }

    public void ChangeCharacters()
    {
        ActiveAnimal.toggleControl();
        ActiveAnimalIndex = (Animal)(((int)ActiveAnimalIndex + 1) % characters.Length);
        ActiveAnimal = characters[(int)ActiveAnimalIndex];
        ActiveAnimal.toggleControl();
        UpdateCamera();
    }
    public void ChangeCharacters(Animal animal)
    {
        ActiveAnimal.toggleControl();
        ActiveAnimalIndex = animal;
        ActiveAnimal = characters[(int)ActiveAnimalIndex];
        ActiveAnimal.toggleControl();
        UpdateCamera();
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

    private void Run()
    {
        ActiveAnimal.toggleRunning();
    }

    private void SetupCamera()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        //Setup Camera Distance
        ActiveAnimal.myCamera.m_Orbits[0].m_Radius = Mathf.Clamp(cameraDistance,1f, 15f);
        ActiveAnimal.myCamera.m_Orbits[1].m_Radius = Mathf.Clamp(cameraDistance,2f, 15f);
        ActiveAnimal.myCamera.m_Orbits[2].m_Radius = Mathf.Clamp(cameraDistance,2f, 15f);
    }

    private void UpdateCamera()
    {
        //Setup Camera Distance
        ActiveAnimal.myCamera.m_Orbits[0].m_Radius = Mathf.Clamp(cameraDistance,1f, 15f);
        ActiveAnimal.myCamera.m_Orbits[1].m_Radius = Mathf.Clamp(cameraDistance+0.3f*cameraDistance,2f, 15f);
        ActiveAnimal.myCamera.m_Orbits[2].m_Radius = Mathf.Clamp(cameraDistance,2f, 15f);
    }

    private void ChangeZoom(float increment)
    {
        cameraDistance = Mathf.Clamp(cameraDistance+increment, 0f, 15f);
    }
    
    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Character.SwitchCharacter.performed += ctx => ChangeCharacters();
        inputActions.Character.Jump.performed += ctx => Jump();
        inputActions.Character.Ability1.performed += ctx => Ability1();
        inputActions.Character.Ability2.performed += ctx => Ability2();
        inputActions.Character.Run.performed += ctx => Run();
        inputActions.Character.Run.canceled += ctx => Run();
        inputActions.Character.Zoom.performed += ctx => ChangeZoom(inputActions.Character.Zoom.ReadValue<float>());
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Character.SwitchCharacter.performed -= ctx => ChangeCharacters();
        inputActions.Character.Jump.performed -= ctx => Jump();
        inputActions.Character.Ability1.performed -= ctx => Ability1();
        inputActions.Character.Ability2.performed -= ctx => Ability2();
        inputActions.Character.Run.performed -= ctx => Run();
        inputActions.Character.Run.canceled -= ctx => Run();
        inputActions.Character.Zoom.performed -= ctx => ChangeZoom(inputActions.Character.Zoom.ReadValue<float>());
    }
}

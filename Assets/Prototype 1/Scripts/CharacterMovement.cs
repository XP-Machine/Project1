using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  This Script reads the Input from the InputActions class and moves the Character Accordingly
//  Created by Laurent Klein
public class CharacterMovement : MonoBehaviour
{
    #region Declarations
    //Public
    [Header("Character Settings")]
    public float movementSpeed = 5f;

    //Private
    private PlayerInput input;
    private Rigidbody rb;

    #endregion Declarations

    private void Awake()
    {
        input = new PlayerInput();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movementInputValue = input.Character.Move.ReadValue<float>();
        MoveCharacter(movementInputValue);
    }

    //MoveCharacter moves the character's RB on the X axis according to float parameter
    private void MoveCharacter(float movementDirection)
    {
        Debug.Log("Input Value: " + movementDirection);
        Vector3 directionVector = new Vector3(movementDirection, 0.0f, 0.0f);
        rb.MovePosition(transform.position + directionVector * movementSpeed * Time.deltaTime);
    }


    #region InputAction Functions
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    #endregion InputAction Functions
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public abstract class animal_base : MonoBehaviour
{
    public string animalName;
    public float speed;
    public float runningSpeed;
    public float rotationSpeed = 2;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public CharacterController controller;
    public manager_animals animalControlManager;
    public CinemachineFreeLook myCamera;

    protected manager_animals.Animal animalType;

    protected float verticalVelocity; // Stores the upward/downward velocity
    protected bool isGrounded;
    protected bool isControlled;
    protected bool isStacked;
    protected bool isRunning = false;

    protected float StackingTimer;
    protected bool Stacking;
    public float StackTimerDuration = 1f;

    public Transform AnimalAnchor;

    private Transform cameraTransform;

    private void Awake()
    {
       cameraTransform = FindObjectOfType<Camera>().transform;
    }

    protected string[] AnimalTags = new string[4] { "Rooster", "Cat", "Dog", "Donkey" };
    public virtual void Move(Vector2 inputVect)
    {
        if (isStacked) return;
        isGrounded = controller.isGrounded; // Check if character is on the ground

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Reset vertical velocity when grounded
        }

        Vector3 move = cameraTransform.forward * inputVect.y + cameraTransform.right * inputVect.x;
        if (isRunning)
        {
            move = runningSpeed * move;
        }
        else
        {
            move = speed * move;
        }
        // Apply gravity and move the character
        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
        
        //Rotate model
        move.y = 0f; //Remove vertical component
        float angleToRotate = Quaternion.FromToRotation(transform.forward, move).eulerAngles.y; //Calculate angle
        angleToRotate -= 180; //Adjust angles between -180 and 180
        if (move != Vector3.zero)
        {
            transform.Rotate(Vector3.up,angleToRotate*Time.deltaTime * rotationSpeed);
        }
    }

    public virtual void Jump()
    {
        //Stack off if I am stacked
        if (isStacked)
        {
            //Take animal on me and stack it on the animal below me
            if (AnimalAnchor.childCount > 0)
            {
                animal_base animalOnMe = AnimalAnchor.GetComponentInChildren<animal_base>();
                animalOnMe.transform.SetParent(transform.parent);
                animalOnMe.transform.localPosition = Vector3.zero;
            }

            //Unstack me nd jump
            transform.SetParent(null);
            isStacked = false;
            gameObject.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<CharacterController>().enabled = true;
        }
        
        if (isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public virtual void AbilityX()
    {
    }

    public virtual void AbilityY()
    {
    }

    public virtual void Update()
    {
        if (isStacked) return;
        if (!isControlled ) Move(Vector3.zero);
    }

    public virtual void Start()
    {
        
    }

    public void toggleControl()
    {
        myCamera.gameObject.SetActive(!myCamera.gameObject.activeSelf);
        isControlled = !isControlled;
    }

    public void toggleRunning()
    {
        isRunning = !isRunning;
    }

    public void startStacking(manager_animals.Animal otherAnimal)
    {
        if (AnimalAnchor.childCount > 0)
        {
            animal_base childAnimal1 = AnimalAnchor.GetComponentInChildren<animal_base>();
            if(childAnimal1.AnimalAnchor.childCount > 0)
            {
                animal_base childAnimal2 = AnimalAnchor.GetComponentInChildren<animal_base>();
                childAnimal2.stackMe(otherAnimal);
            }
            childAnimal1.stackMe(otherAnimal);
        }
        stackMe(otherAnimal);
    }


    public void stackMe(manager_animals.Animal otherAnimal)
    {
        //Get the Gameobject of the target animal
        GameObject stackTargetBase;
        switch (otherAnimal)
        {
            case manager_animals.Animal.Donkey:
                stackTargetBase = FindObjectOfType<animal_donkey>().gameObject;
                break;
            case manager_animals.Animal.Dog:
                stackTargetBase = FindObjectOfType<animal_dog>().gameObject;
                break;
            case manager_animals.Animal.Cat:
                stackTargetBase = FindObjectOfType<animal_cat>().gameObject;
                break;
            case manager_animals.Animal.Rooster:
                stackTargetBase = FindObjectOfType<animal_rooster>().gameObject;
                break;
            default:
                stackTargetBase = null;
                break;
        }

        if (stackTargetBase == null){return;}

        //If smaller then target
        if ((int)animalType < (int)stackTargetBase.GetComponent<animal_base>().animalType)
        {
            //Check first layer child
            animal_base targetScript = stackTargetBase.GetComponent<animal_base>();
            
            for (int i = 0; i < 2; i++)
            {
                if (targetScript.AnimalAnchor.childCount > 0)
                {
                    //IF Child is bigger than me remember the child
                    if ((int)animalType < (int)targetScript.AnimalAnchor.GetComponentInChildren<animal_base>().animalType)
                    {
                        targetScript = targetScript.AnimalAnchor.GetComponentInChildren<animal_base>();
                    }
                    //If child is smaller than me put it on my back
                    else if((int)animalType > (int)targetScript.AnimalAnchor.GetComponentInChildren<animal_base>().animalType)
                    {
                        animal_base childScript = targetScript.AnimalAnchor.GetComponentInChildren<animal_base>();
                        childScript.transform.SetParent(AnimalAnchor);
                        childScript.transform.localPosition = Vector3.zero;
                        childScript.transform.localRotation = Quaternion.identity;
                        childScript.isStacked = true;
                        childScript.GetComponent<BoxCollider>().enabled = false;
                        childScript.GetComponent<CharacterController>().enabled = false;
                    }
                }
            }
            
            //Stack on the target animal
            transform.SetParent(targetScript.AnimalAnchor);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            isStacked = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<CharacterController>().enabled = false;
            changeControl(otherAnimal);
        } 
        //if Bigger then target
        else
        {
            //Put target on me
            animal_base targetScript = stackTargetBase.GetComponent<animal_base>();
            stackTargetBase.transform.SetParent(transform);
            stackTargetBase.transform.localPosition = Vector3.zero;
            stackTargetBase.transform.localRotation = Quaternion.identity;
            targetScript.isStacked = true;
            stackTargetBase.GetComponent<BoxCollider>().enabled = false;
            stackTargetBase.GetComponent<CharacterController>().enabled = false;
        }
    }

    private void changeControl(manager_animals.Animal otherAnimal)
    {
        animalControlManager.ChangeCharacters((manager_animals.Animal)(int)otherAnimal);
    }
    public void OnTriggerEnter(Collider other)
    {
        manager_animals.Animal otherAnimal = other.gameObject.GetComponent<animal_base>().animalType;
        if ((int)otherAnimal <= (int)animalType) return;
        if(AnimalTags.Contains<string>(other.gameObject.tag))
        {
            StackingTimer = 0;
            Stacking = true;
        }
        
    }
    public void OnTriggerStay(Collider other)
    {
        if (!Stacking) return;

        StackingTimer += Time.deltaTime;
        if (StackingTimer >= StackTimerDuration)
        {
            startStacking(other.gameObject.GetComponent<animal_base>().animalType);
            Stacking = false;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        Stacking = false;
        StackingTimer = -1f;
    }
}

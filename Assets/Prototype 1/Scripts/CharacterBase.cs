using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public string animalName;
    public float speed;
    public float rotationSpeed = 2;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public CharacterController controller;
    public AnimalControlManager animalControlManager;

    protected StackManager.Animal animalType;

    protected float verticalVelocity; // Stores the upward/downward velocity
    protected bool isGrounded;
    protected bool isControlled;
    protected bool isStacked;

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
   // public GameObject[] AnimalGameObjects = new GameObject[4];
    public virtual void Move(Vector2 inputVect)
    {
        if (isStacked) return;
        isGrounded = controller.isGrounded; // Check if character is on the ground

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Reset vertical velocity when grounded
        }

        Vector3 move = cameraTransform.forward * inputVect.y + cameraTransform.right * inputVect.x;
        move = speed * move;
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
                CharacterBase animalOnMe = AnimalAnchor.GetComponentInChildren<CharacterBase>();
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
        isControlled = !isControlled;
    }

    public void startStacking(StackManager.Animal otherAnimal)
    {
        if (AnimalAnchor.childCount > 0)
        {
            CharacterBase childAnimal1 = AnimalAnchor.GetComponentInChildren<CharacterBase>();
            if(childAnimal1.AnimalAnchor.childCount > 0)
            {
                CharacterBase childAnimal2 = AnimalAnchor.GetComponentInChildren<CharacterBase>();
                childAnimal2.stackMe(otherAnimal);
            }
            childAnimal1.stackMe(otherAnimal);
        }
        stackMe(otherAnimal);
    }


    public void stackMe(StackManager.Animal otherAnimal)
    {
        //Get the Gameobject of the target animal
        GameObject stackTargetBase;
        switch (otherAnimal)
        {
            case StackManager.Animal.Donkey:
                stackTargetBase = FindObjectOfType<DonkeyAnimal>().gameObject;
                break;
            case StackManager.Animal.Dog:
                stackTargetBase = FindObjectOfType<DogAnimal>().gameObject;
                break;
            case StackManager.Animal.Cat:
                stackTargetBase = FindObjectOfType<CatAnimal>().gameObject;
                break;
            case StackManager.Animal.Rooster:
                stackTargetBase = FindObjectOfType<RoosterAnimal>().gameObject;
                break;
            default:
                stackTargetBase = null;
                break;
        }

        if (stackTargetBase == null){return;}

        //If smaller then target
        if ((int)animalType < (int)stackTargetBase.GetComponent<CharacterBase>().animalType)
        {
            //Check first layer child
            CharacterBase targetScript = stackTargetBase.GetComponent<CharacterBase>();
            
            for (int i = 0; i < 2; i++)
            {
                if (targetScript.AnimalAnchor.childCount > 0)
                {
                    //IF Child is bigger than me remember the child
                    if ((int)animalType < (int)targetScript.AnimalAnchor.GetComponentInChildren<CharacterBase>().animalType)
                    {
                        targetScript = targetScript.AnimalAnchor.GetComponentInChildren<CharacterBase>();
                    }
                    //If child is smaller than me put it on my back
                    else if((int)animalType > (int)targetScript.AnimalAnchor.GetComponentInChildren<CharacterBase>().animalType)
                    {
                        CharacterBase childScript = targetScript.AnimalAnchor.GetComponentInChildren<CharacterBase>();
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
            CharacterBase targetScript = stackTargetBase.GetComponent<CharacterBase>();
            stackTargetBase.transform.SetParent(transform);
            stackTargetBase.transform.localPosition = Vector3.zero;
            stackTargetBase.transform.localRotation = Quaternion.identity;
            targetScript.isStacked = true;
            stackTargetBase.GetComponent<BoxCollider>().enabled = false;
            stackTargetBase.GetComponent<CharacterController>().enabled = false;
        }
    }

    private void changeControl(StackManager.Animal otherAnimal)
    {
        animalControlManager.ChangeCharacters((AnimalControlManager.Animal)(int)otherAnimal);
    }
    public void OnTriggerEnter(Collider other)
    {
        StackManager.Animal otherAnimal = other.gameObject.GetComponent<CharacterBase>().animalType;
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
            startStacking(other.gameObject.GetComponent<CharacterBase>().animalType);
            Stacking = false;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        Stacking = false;
        StackingTimer = -1f;
    }
}

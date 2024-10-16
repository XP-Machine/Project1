using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public string animalName;
    public float speed;
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

        Vector3 move = new Vector3(inputVect.x, 0, inputVect.y);
        move = speed * move;
        // Apply gravity and move the character
        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }

    public virtual void Jump()
    {
        if (isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log(animalName + " jumped!");
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
      //  if (isStacked) return;
      //  if (!isControlled ) Move(Vector3.zero);
    }

    public virtual void Start()
    {
        print(AnimalAnchor ? "the" + animalName + "has an anchor" : animalName + "NO ANCHOR");
    }

    public void toggleControl()
    {
        isControlled = !isControlled;
    }

    public void stackMe(StackManager.Animal otherAnimal, GameObject animal)
    {
        if ((int)otherAnimal <= (int)animalType) return;

        Transform stackAnchor = animal.GetComponent<CharacterBase>().AnimalAnchor;

        if (stackAnchor == null)
        {
            return;
        }

        transform.SetParent(stackAnchor);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
  //      print(transform.localPosition);
        isStacked = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<CharacterController>().enabled = false;
        changeControl(otherAnimal);
    }

    private void changeControl(StackManager.Animal otherAnimal)
    {
        animalControlManager.ChangeCharacters((AnimalControlManager.Animal)(int)otherAnimal);
    }
    public void OnTriggerEnter(Collider other)
    {
        StackManager.Animal otherAnimal = other.gameObject.GetComponent<CharacterBase>().animalType;
        if ((int)otherAnimal <= (int)animalType) return;
        print(animalType + " is trying to mount " +otherAnimal);


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
            stackMe(other.gameObject.GetComponent<CharacterBase>().animalType, other.gameObject);
            Stacking = false;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        Stacking = false;
        StackingTimer = -1f;
    }
}

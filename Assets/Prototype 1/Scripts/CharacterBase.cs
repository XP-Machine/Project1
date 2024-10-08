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

    protected float verticalVelocity; // Stores the upward/downward velocity
    protected bool isGrounded;
    protected bool isControlled;

    protected float StackingTimer;
    protected bool Stacking;
    public float StackTimerDuration = 1f;

    protected string[] AnimalTags = new string[4] { "Rooster", "Cat", "Dog", "Donkey" };

    public virtual void Move(Vector2 inputVect)
    {
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
        if (!isControlled) Move(Vector3.zero);
    }

    public void toggleControl()
    {
        isControlled = !isControlled;
    }


    public void OnTriggerEnter(Collider other)
    { 
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
            print("Timer Reached");
            StackManager.VisualizeStack();
            StackManager.StackOn(StackManager.Animal.Dog, StackManager.Animal.Cat);
            StackManager.VisualizeStack();
            //Debug.Log(StackManager.GetLocation(StackManager.Animal.Dog));
            Stacking = false;
        }
    }
}

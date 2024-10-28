using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterAnimal : CharacterBase
{
    public float interactionRadius = 1;
    public float gravityReductionRate = 0.3f;
    public bool isGliding = false;
    public override void Start()
    {
        base.Start();
        animalType = StackManager.Animal.Rooster;
        AnimalTags = new string[] { "Cat", "Dog", "Donkey" };
    }
    

    public override void Move(Vector2 inputVect)
    {
        if (isGliding)
        {
            gravity *= gravityReductionRate;
        }
        
        base.Move(inputVect);

        gravity = -9.81f;
        if (isGrounded)
        {
            isGliding = false;
        }
    }
    
    public override void AbilityX()
    {
        base.AbilityX();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("InteractableButton"))
            {
                print("ButtonPressed");
                collider.gameObject.GetComponent<OpenGate>().openGate();
            }
        }
    }

    public override void AbilityY()
    {
        base.AbilityY();
        if (!isGrounded)
        {
            isGliding = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}

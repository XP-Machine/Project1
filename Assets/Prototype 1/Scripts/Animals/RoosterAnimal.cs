using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterAnimal : CharacterBase
{
    public float interactionRadius = 1;
    public void Start()
    {
        base.Start();
        animalType = StackManager.Animal.Rooster;
        AnimalTags = new string[] { "Cat", "Dog", "Donkey" };
    }
    public override void Move(Vector2 inputVect)
    {
        base.Move(inputVect);
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
                break;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}

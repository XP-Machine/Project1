using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimal : CharacterBase
{
    public float interactionRadius = 1;
    public void Start()
    {
        base.Start();
        animalType = StackManager.Animal.Cat;
        AnimalTags = new string[] { "Dog", "Donkey" };
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
            if (collider.CompareTag("InteractableLever"))
            {
                print("LeverPressed");
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }


}

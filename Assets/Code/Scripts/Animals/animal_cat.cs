using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class animal_cat : animal_base
{
    public float interactionRadius = 1;
    public override void Start()
    {
        base.Start();
        animalType = manager_animals.Animal.Cat;
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
                collider.gameObject.GetComponent<environment_button>().openGate();
            }
        }
    }
    
    public override void AbilityY()
    {
        base.AbilityY();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("InteractableWall"))
            {
                StartCoroutine(ClimbWall(collider.gameObject));
            }
        }
    }

    //To be deleted - Teleport to opposite side of wall
    IEnumerator ClimbWall(GameObject wall)
    {
        //Unstack on top of me
        if (AnimalAnchor.childCount > 0)
        {
            animal_base childAnimal1 = AnimalAnchor.GetComponentInChildren<animal_base>();
            childAnimal1.Jump();
        }

        controller.enabled = false;
        print("Wall Climb activated");
        Vector3 wallPosition = wall.transform.position;
        wallPosition.y = 0f;
        if (transform.position.z > wallPosition.z)
        {
            transform.position = wallPosition + new Vector3(0f, 0f, 2f);
        }
        else
        {
            transform.position = wallPosition + new Vector3(0f, 0f, -2f);
        }
        yield return new WaitForSeconds(2f);
        print("Swapping sides");
        if (transform.position.z > wallPosition.z)
        {
            transform.position = wallPosition + new Vector3(0f, 0f, -2f);
        }
        else
        {
            transform.position = wallPosition + new Vector3(0f, 0f, 2f);
        }
        controller.enabled = true;

    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }


}

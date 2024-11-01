using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animal_dog : animal_base
{
    private CharacterController _Controller;
    public override void Start()
    {
        base.Start();
        animalType = manager_animals.Animal.Dog;
        AnimalTags = new string[] {"Donkey"};
        _Controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update

    public override void Move(Vector2 inputVect)
    {
        base.Move(inputVect);

    }
    public override void AbilityX()
    {
        base.AbilityX();
        RaycastHit hit;
        if (Physics.Raycast(transform.position+new Vector3(0,0.5f,0), -transform.forward, out hit, 3f))
        {
            // Check if the object hit has the tag "digSpot"
            if (hit.collider.gameObject.tag == "DigSpot")
            {
                if (AnimalAnchor.childCount > 0)
                {
                    animal_base childAnimal1 = AnimalAnchor.GetComponentInChildren<animal_base>();
                    childAnimal1.Jump();
                }
                // Access the DigSpot script on the hit object
                environment_digLocation digSpotScript = hit.collider.gameObject.GetComponent<environment_digLocation>();
                if (digSpotScript != null && digSpotScript.LinkedDigSpot != null)
                {
                    print("Moving");
                    _Controller.enabled = false;
                    // Move the current GameObject to the position of the LinkedDigSpot
                    transform.position = digSpotScript.LinkedDigSpot.transform.position;
                    _Controller.enabled = true;
                }
                else
                {
                    Debug.LogWarning("LinkedDigSpot is not assigned or DigSpot script is missing!");
                }
            }
            print(hit.collider.gameObject.tag);
           
        }
    }

    void OnDrawGizmos()
    {
        // Set the color for the ray
        Gizmos.color = Color.red;

        // Draw a line representing the raycast direction and distance
        Vector3 forwardDirection = -transform.forward * 3f;
        Gizmos.DrawLine(transform.position + new Vector3(0, 0.5f, 0), transform.position + new Vector3(0, 0.5f, 0) + forwardDirection);
    }
}

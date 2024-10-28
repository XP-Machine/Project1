using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyAnimal : CharacterBase
{
    public float pushForce = 5f; 
    public float raycastDistance = 1f; 
    public LayerMask pushableLayerMask; 

    public override void Start()
    {
        base.Start();
        animalType = StackManager.Animal.Donkey;
        AnimalTags = new string[] { };
    }
    public override void Move(Vector2 inputVect)
    {
        base.Move(inputVect);
        PushForward();

    }
    void PushForward()
    {
        // Perform a forward raycast with the specified distance and layer mask
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0,1,0), -transform.forward, out hit, raycastDistance, pushableLayerMask))
        {
            // Check if the hit object has a Rigidbody
            Rigidbody hitRb = hit.collider.GetComponent<Rigidbody>();
            if (hitRb != null)
            {
                // Apply a forward force to the hit object
                Vector3 pushDirection = -transform.forward * pushForce;
                hitRb.AddForce(pushDirection, ForceMode.Impulse);
            }
        }
    }
    public override void AbilityX()
    {
        base.AbilityX();

    }

    public override void AbilityY()
    {
        base.AbilityY();
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out hit, 3f))
        {
            // Check if the object hit has the tag "digSpot"
            if (hit.collider.gameObject.tag == "Breakable")
            {
                print("Break");
                Destroy(hit.collider.gameObject);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Visualize the raycast in the Scene view
        Gizmos.color = Color.blue;
        Vector3 forwardDirection = -transform.forward * raycastDistance;
        Gizmos.DrawLine(transform.position, transform.position + forwardDirection);
    }
}

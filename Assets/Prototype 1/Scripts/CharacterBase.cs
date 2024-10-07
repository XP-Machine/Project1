using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public string animalName;
    public float speed;
    public float gravity;
    public CharacterController controller;

    public virtual void Move(Vector2 inputVect)
    {
        print(animalName);
        controller.Move(new Vector3(inputVect.x, gravity, inputVect.y) * speed * Time.deltaTime);
    }
    public virtual void AbilityX()
    {

    }
    public virtual void AbilityY()
    {

    }
}

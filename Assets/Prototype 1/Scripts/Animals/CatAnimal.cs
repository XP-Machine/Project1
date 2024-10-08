using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimal : CharacterBase
{
    public void Start()
    {
        AnimalTags = new string[] { "Dog", "Donkey" };
    }
    public override void Move(Vector2 inputVect)
    {
        base.Move(inputVect);

    }
    public override void AbilityX()
    {
        base.AbilityX();
    }
}

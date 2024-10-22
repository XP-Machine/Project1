using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyAnimal : CharacterBase
{
    public void Start()
    {
        base.Start();
        animalType = StackManager.Animal.Donkey;
        AnimalTags = new string[] {};
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

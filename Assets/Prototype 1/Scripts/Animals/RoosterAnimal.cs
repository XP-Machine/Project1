using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterAnimal : CharacterBase
{
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
    }
}

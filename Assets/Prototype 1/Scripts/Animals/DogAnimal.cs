using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimal : CharacterBase
{
    public void Start()
    {
        base.Start();
        animalType = StackManager.Animal.Dog;
        AnimalTags = new string[] {"Donkey"};
    }
    // Start is called before the first frame update

    public override void Move(Vector2 inputVect)
    {
        base.Move(inputVect);

    }
    public override void AbilityX()
    {
        base.AbilityX();
    }
}

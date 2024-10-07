using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalControlManager : MonoBehaviour
{
    public enum Animal
    {
        Rooster,
        Cat,
        Dog,
        Donkey,
    };

    public CharacterMovement[] characterMovements = new CharacterMovement[4];

    private Animal currentAnimal = 0;



}

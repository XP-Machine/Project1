using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StackManager
{
    
    public enum Animal
    {
        Rooster,
        Cat,
        Dog,
        Donkey, 
    };
/*
    private static Animal[,] stacks = { { Animal.Empty, Animal.Empty, Animal.Empty, Animal.Empty }, 
                                        { Animal.Empty, Animal.Empty, Animal.Empty, Animal.Empty }, 
                                        { Animal.Empty, Animal.Empty, Animal.Empty, Animal.Empty }, 
                                        { Animal.Empty, Animal.Empty, Animal.Empty, Animal.Empty } };


    public static void VisualizeStack()
    {
        Debug.Log("Row " + 4 + ": " + stacks[0,3] + " | " + stacks[1,3] + " | " + stacks[2,3] + " | "+ stacks[3,3] + " | \n" +
                  "Row " + 3 + ": " + stacks[0,2] + " | " + stacks[1,2] + " | " + stacks[2,2] + " | "+ stacks[3,2] + " | \n" +
                  "Row " + 2 + ": " + stacks[0,1] + " | " + stacks[1,1] + " | " + stacks[2,1] + " | "+ stacks[3,1] + " | \n" +
                  "Row " + 1 + ": " + stacks[0,0] + " | " + stacks[1,0] + " | " + stacks[2,0] + " | "+ stacks[3,0] + " | \n");
    }
    public static Vector2 GetLocation(Animal animal)
    {
        Vector2 returnValue = new Vector2(4, 4);

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (stacks[x, y] == animal)
                {
                    returnValue.x = x;
                    returnValue.y = y;
                }
            }
        }
        return returnValue;
    }

    public static void Initialize()
    {
        stacks[0, 0] = Animal.Donkey;
        stacks[1, 0] = Animal.Dog;
        stacks[2, 0] = Animal.Cat;
        stacks[3, 0] = Animal.Rooster;
    }

    public static void StackOn(Animal animalToBeStacked, Animal stackTarget)
    {
        //Identify the stack the target is in and delete the Animal to be stacked
        Queue<Animal> animalQueue = new Queue<Animal>();
        int targetStack = 4;
        
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (stacks[x, y] == animalToBeStacked)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        animalQueue.Enqueue(stacks[x,i]);
                        stacks[x, i] = Animal.Empty;
                    }
                }
                
                if (stacks[x, y] == stackTarget)
                {
                    targetStack = x;
                }
            }
        }
        
        //Stack on new stack
        while (animalQueue.Count > 0)
        {
            //If empty dequeue immedeatly and continue
            if (animalQueue.Peek() == Animal.Empty)
            {
                animalQueue.Dequeue();
                continue;
            }
            
            //Bubble Sort Down
            stacks[targetStack, 3] = animalQueue.Dequeue(); //Set on top of stack
            for (int y = 2; y >= 0; y--)
            {
                if (stacks[targetStack, y+1] > stacks[targetStack, y])
                {
                    Animal rememberAnimal = stacks[targetStack, y];
                    stacks[targetStack, y] = stacks[targetStack, y + 1];
                    stacks[targetStack, y + 1] = rememberAnimal;
                }
            }
        }
    }

    public static void StackOff(Animal animalToStackOff)
    {
        //Find the animal, remember stack number and put animal into first empty stack
        int targetStack = 4;
        //bool emptyStackFound = false;
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (stacks[x, y] == animalToStackOff)
                {
                    targetStack = x;
                }
                
                //Put animal on first empty stack
              //  if (stacks[x, 0] == Animal.Empty && !emptyStackFound)
             //   {
             //       stacks[x, 0] = animalToStackOff;
             //       emptyStackFound = true;
             //   }
            }
        }
        //Remove it and stack everything down
        for (int i = 3; i >= 0; i--)
        {
            //Check if target animal
            if (stacks[targetStack, i] == animalToStackOff)
            {
               // stacks[targetStack, i] = Animal.Empty;
            }
        }
        //Bubble sort
        for (int y = 2; y >= 0; y--)
        {
            if (stacks[targetStack, y+1] > stacks[targetStack, y])
            {
                Animal rememberAnimal = stacks[targetStack, y];
                stacks[targetStack, y] = stacks[targetStack, y + 1];
                stacks[targetStack, y + 1] = rememberAnimal;
            }
        }
    }
*/
}

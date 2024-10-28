using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour
{

    public GameObject door;
    // Start is called before the first frame update

    public void openGate()
    {
        Destroy(door);
        if(door){Debug.Log("Test");}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environment_liftInstance : MonoBehaviour
{
    public environment_Lift liftManager;
    public manager_animals animals;
    public int weightReq = 3;
    public int weight = 0;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        weight += other.gameObject.GetComponent<animal_base>().getWeight();
        if(weight >= weightReq) liftManager.liftActive = true;

    }
    private void OnTriggerExit(Collider other)
    {
        weight -= other.gameObject.GetComponent<animal_base>().getWeight();
        if (weight < weightReq) liftManager.liftActive = false;
    }
}

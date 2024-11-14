using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animal_StackDebug : MonoBehaviour
{
    public GameObject animal;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 boxSize = animal.GetComponent<BoxCollider>().size;
        boxSize.y = 0.1f;
        transform.localScale = boxSize;

    }

    private void Update()
    {
        GetComponent<MeshRenderer>().enabled = !animal.GetComponent<animal_base>().getStacked();
        transform.position = new Vector3(transform.position.x, 0.01f, transform.position.z);
    }

}

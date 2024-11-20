using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animal_StackDebug : MonoBehaviour
{
    public GameObject animal;
    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    private void Update()
    {

        Vector3[] points = new Vector3[2];
        points[0] = animal.transform.position + new Vector3(0, 1, 0);
        points[1] = points[0] - transform.forward * 5f;
        lineRenderer.SetPositions(points);
    }

}

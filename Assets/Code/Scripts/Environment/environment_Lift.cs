using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environment_Lift : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public GameObject lift1, lift2;
    public Vector3 lift1StartPos, lift2StartPos;

    private float liftHeight;
    [Range(0,1)]
    public float time = 0;
    public bool liftActive;

    // Start is called before the first frame update
    void Start()
    {
        liftHeight = lift2.transform.position.y - lift1.transform.position.y;
        lift1StartPos = lift1.transform.position;
        lift2StartPos = lift2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(liftActive)
        {
            if (time < 1) time += Time.deltaTime;
            // Evaluate the curve to get the movement factor (0 to 1)
            float curveValue = animationCurve.Evaluate(time);

            // Calculate new positions based on the curve value
            lift1.transform.position = lift1StartPos + Vector3.up * curveValue * liftHeight;
            lift2.transform.position = lift2StartPos - Vector3.up * curveValue * liftHeight;
        }
        if (!liftActive)
        {
            if (time >0) time -= Time.deltaTime;
            // Evaluate the curve to get the movement factor (0 to 1)
            float curveValue = animationCurve.Evaluate(time);

            // Calculate new positions based on the curve value
            lift1.transform.position = lift1StartPos + Vector3.up * curveValue * liftHeight;
            lift2.transform.position = lift2StartPos - Vector3.up * curveValue * liftHeight;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        liftActive = true;
    }
    private void OnTriggerExit(Collider other)
    {
        liftActive = false;
    }
}

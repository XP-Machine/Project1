using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("References")]
    public AnimalControlManager acm;

    [Header("Variables")] public float cameraDistance = 10;

    private Transform _activeAnimal;
    private CinemachineFreeLook _camera;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get References
        _camera = GetComponent<CinemachineFreeLook>();
        
        //Setup Camera Distance
        _camera.m_Orbits[0].m_Radius = cameraDistance;
        _camera.m_Orbits[1].m_Radius = cameraDistance;
        _camera.m_Orbits[2].m_Radius = cameraDistance;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the current active animal
        _activeAnimal = acm.ActiveAnimal.transform;
        
        //Follow active Animal
        _camera.Follow = _activeAnimal;
        _camera.LookAt = _activeAnimal;
    }
}

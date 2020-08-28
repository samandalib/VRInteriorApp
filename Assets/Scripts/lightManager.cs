using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script enables a button to be able to turn on/off a light
/// </summary>
public class lightManager : MonoBehaviour
{
    public GameObject activeObjectManager;

    [SerializeField]
    private GameObject activeGameObject;
    private ActiveObjectManager _script;

    [SerializeField]
    private Light[] _lights;

    private void Update()
    {
        _script = activeObjectManager.GetComponent<ActiveObjectManager>();
        activeGameObject = _script.activeGameObject;
        _lights = activeGameObject.transform.parent.GetComponent<ActiveObjectDependancies>().lights;
    }

    public void ChangeStatus()
    {


        if(_lights != null)
        {
            Debug.Log("Lights Length is ::::::::::::::::::::::::::::::::::::::::::::" + _lights.Length);
            for (int i=0; i <_lights.Length; i++)
            {
                _lights[i].enabled = !_lights[i].enabled;
                
            }
            
        }

        

    }
}

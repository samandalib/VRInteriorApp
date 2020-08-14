using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script enables a button to be able to turn on/off a light
/// </summary>
public class lightManager : MonoBehaviour
{
    public GameObject activeGameObject;
    private ActiveObjectManager _script;
    private Light _light;

    public void ChangeStatus()
    {
        _script = activeGameObject.GetComponent<ActiveObjectManager>();
        _light = _script.activeGameObject.transform.GetComponentInChildren<Light>();

        _light.enabled = !_light.enabled;

    }

}

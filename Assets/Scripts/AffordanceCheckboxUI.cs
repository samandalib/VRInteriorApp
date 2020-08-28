using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is aimed to sync object affordances with Checkbox UI and inspector scripts for each object
public class AffordanceCheckboxUI : MonoBehaviour
{
    private GameObject _activeObject;
    private Transform _parentObject;


    public GameObject floorObjectToggle;
    public GameObject wallObjectToggle;
    public GameObject ceilingObjectToggle;
    public GameObject LightObjectToggle;


    private bool _floorObject;
    private bool _wallObject;
    private bool _ceilingObject;
    private bool _lightHolderObject;


    void Update()
    {
        try
        {
            _activeObject = transform.GetComponent<ActiveObjectManager>().activeGameObject;
        }
        catch
        {
            Debug.Log("No active object found for the checkboxUI script");
        }

        if (_activeObject != null)
        {
            _parentObject = _activeObject.transform.parent;

            _floorObject = _parentObject.GetComponent<ObjectAffordances>().floorObject;
            _wallObject = _parentObject.GetComponent<ObjectAffordances>().wallObject;
            _ceilingObject = _parentObject.GetComponent<ObjectAffordances>().ceilingObject;
            _lightHolderObject = _parentObject.GetComponent<ObjectAffordances>().lightHolderObject;


            floorObjectToggle.GetComponent<Toggle>().isOn = _floorObject;
            wallObjectToggle.GetComponent<Toggle>().isOn = _wallObject;
            ceilingObjectToggle.GetComponent<Toggle>().isOn = _ceilingObject;
            LightObjectToggle.GetComponent<Toggle>().isOn = _lightHolderObject;

            Debug.Log("From Update method in CHeckboxUI script");
        }



    }

    //When any of the Checkboxes are changed, one of the functions below will run depending on the object type

     //if the object is a floor object
    public void onCheckFloor()
    {

        bool currentStatus = floorObjectToggle.GetComponent<Toggle>().isOn;

        if (currentStatus)
        {
            _parentObject.GetComponent<ObjectAffordances>().floorObject = false;
            floorObjectToggle.GetComponent<Toggle>().isOn = false;
            currentStatus = false;
        }
        else
        {
            _parentObject.GetComponent<ObjectAffordances>().floorObject = true;
            floorObjectToggle.GetComponent<Toggle>().isOn = true;
            currentStatus = true;
        }
    }

    public void onCheckWall()
    {

        bool currentStatus = wallObjectToggle.GetComponent<Toggle>().isOn;

        if (currentStatus)
        {
            _parentObject.GetComponent<ObjectAffordances>().wallObject = false;
            wallObjectToggle.GetComponent<Toggle>().isOn = false;
            currentStatus = false;
        }
        else
        {
            _parentObject.GetComponent<ObjectAffordances>().wallObject = true;
            wallObjectToggle.GetComponent<Toggle>().isOn = true;
            currentStatus = true;
        }

    }

    public void onCheckCeiling()
    {
        bool currentStatus = ceilingObjectToggle.GetComponent<Toggle>().isOn;

        if (currentStatus)
        {
            _parentObject.GetComponent<ObjectAffordances>().ceilingObject = false;
            ceilingObjectToggle.GetComponent<Toggle>().isOn = false;
            currentStatus = false;
        }
        else
        {
            _parentObject.GetComponent<ObjectAffordances>().ceilingObject = true;
            ceilingObjectToggle.GetComponent<Toggle>().isOn = true;
            currentStatus = true;
        }
    }

    public void onCheckLight()
    { 
        bool currentStatus = LightObjectToggle.GetComponent<Toggle>().isOn;

        if (currentStatus)
        {
            _parentObject.GetComponent<ObjectAffordances>().lightHolderObject = false;
            LightObjectToggle.GetComponent<Toggle>().isOn = false;
            currentStatus = false;
        }
        else
        {
            _parentObject.GetComponent<ObjectAffordances>().lightHolderObject = true;
            LightObjectToggle.GetComponent<Toggle>().isOn = true;
            currentStatus = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ActiveObjectManager : MonoBehaviour
{


    //speed for object movement/rotation/scale
    public int movementSpeed = 2;

    //need to specify the XR Rig to disable locomotion when object is selected
    public GameObject Rig;


    //The Vector2 variable out of the 2DAxis input
    public Vector2 newPosition; 

    //The controller that its 2DAxis will be used for interactions
    public XRController controller;

    GameObject[] activeGameObjects;
    public GameObject activeGameObject;

    public GameObject interactionIndicatorCanvas;

    //We will only need the X and Z value of the look direction to decide about movement adjustment
    [SerializeField]
    Vector2 XZLookDirection;

    //get active object type
    public bool floorObject;
    public bool wallObject;
    public bool ceilingObject;

    void Update()
    {
        FindActiveObject();

    }

    //check and find if any GameObject is active for interaction in the scene
    void FindActiveObject()
    {
        try
        {
            activeGameObjects = GameObject.FindGameObjectsWithTag("ActiveObject");

            //Only keep the last selected object as the ActiveObject
            activeGameObject = activeGameObjects[activeGameObjects.Length - 1];
            if (activeGameObjects.Length > 1)
            {
                for (int i = 0; i < (activeGameObjects.Length - 1); i++)
                {
                    activeGameObjects[i].tag = "Untagged";
                }
            }

            //Check to see if the active object is on wall/floor/ceiling
            FindActiveObjectConstraints();

        }
        catch
        {
            activeGameObject = null;    
        }      
    }

    void FindActiveObjectConstraints()
    {
        floorObject = activeGameObject.transform.GetComponentInParent<ObjectAffordances>().floorObject;
        wallObject =  activeGameObject.transform.GetComponentInParent<ObjectAffordances>().wallObject;
        ceilingObject =  activeGameObject.transform.GetComponentInParent<ObjectAffordances>().ceilingObject;
    }


    //Untag all active objects if no object is selected
    //This function will be used by StatusShow object
    public void UntaggActiveObjects()
    {
        activeGameObjects = GameObject.FindGameObjectsWithTag("ActiveObject");
        for (int i = 0; i < activeGameObjects.Length; i++)
        {
            activeGameObjects[i].tag = "Untagged";
        }
    }



    //Check if there is any input from controller
    public void CheckForInput()
    {
        if (controller.enableInputActions)
            CheckForMovement(controller.inputDevice);
    }
    //get the value from 2DAxis Controller
    private void CheckForMovement(InputDevice device)
    {
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
            newPosition =  position;
    }

}

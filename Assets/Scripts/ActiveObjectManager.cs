﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ActiveObjectManager : MonoBehaviour
{
    // to get the layer name of this object
    public GameObject activeObjectManager;

    //speed for object movement/rotation/scale
    public int speed = 2;

    //need to specify the XR Rig to disable locomotion when object is selected
    public GameObject Rig;

    //The Vector2 variable out of the 2DAxis input
    private Vector2 newPosition; 

    //The controller that its 2DAxis will be used for interactions
    public XRController controller;

    //The interaction layer will be identified and the value will be assigned automatically based on the UI selection
    [SerializeField]
    private int _interactionLayer;

    //The selected gameobject for interaction
    [SerializeField]
    private GameObject _activeGameObject;

    [SerializeField]
    private GameObject interactionIndicator;

    //get all the active objects in the scene into an Array
    GameObject[] activeGameObjects;

    void Update()
    {
        FindActiveObject();

        ////////////TO DO: if there is an active gameobject set the interaction layer
        ///else, set the activeObjectManager layer to 0 and 
        ///or find a way other than working with layers, like calling a function in this script when the button is pressed
        
        SetInteractionLayer();
        
    }

    //Untag all active objects if no object is selected
    public void UntaggActiveObjects()
    {
        activeGameObjects = GameObject.FindGameObjectsWithTag("ActiveObject");
        for (int i = 0; i < activeGameObjects.Length; i++)
        {
            activeGameObjects[i].tag = "Untagged";
        }
    }
    //check and find if any GameObject is active for interaction in the scene
    void FindActiveObject()
    {
        try
        {
            activeGameObjects = GameObject.FindGameObjectsWithTag("ActiveObject");

            //Only keep the last selected object as the ActiveObject
            _activeGameObject = activeGameObjects[activeGameObjects.Length - 1];
            if (activeGameObjects.Length > 1)
            {
                for (int i = 0; i < (activeGameObjects.Length - 1); i++)
                {
                    activeGameObjects[i].tag = "Untagged";
                }
            }

        }
        catch
        {
            _activeGameObject = null;    
        }      
    }

    //Set the Interaction Layer based on the UI button selected
    void SetInteractionLayer()
    {
        //If ActiveObjectManager gameobject is active in the scene, try to get the layer int
        try
        {
            if (activeObjectManager.layer == 0)
            {

                DetermineInteraction(0);

                //The 2DAxis on the controller will work for locomotion
                Rig.GetComponent<Locomotion2DAxis>().enabled = true;

                //Enable the set rotation script to maintain the rotation of objects 
                //This might be unnecessary in the future
                _activeGameObject.GetComponent<SetRotationFix>().enabled = false;

            }
            else
            {
                _interactionLayer = activeObjectManager.layer;

                //locomotion will be disabled the 2DAxis on selected controller will work for object interaction
                Rig.GetComponent<Locomotion2DAxis>().enabled = false;
                DetermineInteraction(_interactionLayer);
            }
        }
        catch
        {
            Debug.LogError("Error from Set intertaction layer catched");
        }

    }

    void UpdateInteractioIndicator(string indicator)
    {
        interactionIndicator = GameObject.Find("interactionIndicator");
        interactionIndicator.GetComponent<TMPro.TextMeshProUGUI>().text = indicator;
    }

    public void DetermineInteraction(int layer)
    {
        switch(layer)
        {
            case 0://Default
                Debug.Log("There is no active object to interact with");
                UpdateInteractioIndicator("");
                break;

            case 11://rotate
                UpdateInteractioIndicator("Rotate");
                Debug.Log("Rotation Is selected !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                _activeGameObject.GetComponent<SetRotationFix>().enabled = false;
                CheckForInput();
                DoObjectRotate(_activeGameObject, newPosition);
                break;

            case 12://move
                //Debug.Log("MOVE Is selected ");
                UpdateInteractioIndicator("Move");
                //Rig.GetComponent<Locomotion2DAxis>().controllers[0] = null;
                CheckForInput();
                DoObjectMove(_activeGameObject, newPosition);
                break;

            case 13://scale
                UpdateInteractioIndicator("Scale");
                Debug.Log("SCALE Is selected !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                break;

            case 14://material
                UpdateInteractioIndicator("Material");
                Debug.Log("MATERIAL Is selected !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                break;

            case 15://Drag
                UpdateInteractioIndicator("Drag");
                DoObjectDrag(_activeGameObject);
                break;
        }
            
    }

    //Check if there is any input from controller
    private void CheckForInput()
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


    //Function for moving item on the floor (X-Z plane) using 2DAxis input
    void DoObjectMove(GameObject obj, Vector2 position)
    {
        obj.transform.Translate(new Vector3(position.x, 0, position.y) * Time.deltaTime * speed);
    }


    //To rotate the object around its own Y-Axis(Objects on the floor)
    void DoObjectRotate(GameObject obj, Vector2 position)
    {
        obj.transform.RotateAround(obj.transform.position, new Vector3(0, position.x, 0), 45 * Time.deltaTime);
    }


    void DoObjectDrag(GameObject obj)
    {
        
    }
}

using System.Collections;
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


    void Update()
    {
        FindActiveObject();

        ////////////TO DO: if there is an active gameobject set the interaction layer
        ///else, set the activeObjectManager layer to 0 and 
        ///or find a way other than working with layers, like calling a function in this script when the button is pressed
        
        SetInteractionLayer();
    }

    //check and find if any GameObject is active for interaction in the scene
    void FindActiveObject()
    {
        try
        {
            _activeGameObject = GameObject.FindGameObjectsWithTag("ActiveObject")[0];
        }
        catch
        {
            _activeGameObject = null;    
        }      
    }

    //Set the Interaction Layer based on the UI button selected
    void SetInteractionLayer()
    {
        if (activeObjectManager.layer == 0)
        {
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

    void DetermineInteraction(int layer)
    {
        switch(layer)
        {
            case 0://Default
                Debug.Log("There is no active object to interact with");
                break;
            case 11://rotate
                Debug.Log("Rotation Is selected !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                _activeGameObject.GetComponent<SetRotationFix>().enabled = false;
                CheckForInput();
                DoObjectRotate(_activeGameObject, newPosition);
                break;
            case 12://move
                //Debug.Log("MOVE Is selected ");
                
                //Rig.GetComponent<Locomotion2DAxis>().controllers[0] = null;
                CheckForInput();
                DoObjectMove(_activeGameObject, newPosition);
                break;

            case 13://scale
                Debug.Log("SCALE Is selected !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                break;

            case 14://material
                Debug.Log("MATERIAL Is selected !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                break;

            case 15://Drag
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
        obj.transform.RotateAround(obj.transform.localPosition, new Vector3(0, position.x, 0), 45 * Time.deltaTime);
    }

    void DoObjectDrag(GameObject obj)
    {
        
    }
}

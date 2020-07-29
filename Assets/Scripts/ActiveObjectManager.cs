using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ActiveObjectManager : MonoBehaviour
{
    // to get the layer name of this object
    //public GameObject activeObjectManager;

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

    //Communicate with MovementAdjuster Script to get look direction
    public GameObject AxisAdjuster;
    private MovementAdjuster _axisAdjusterScript;

    //We will only need the X and Z value of the look direction to decide about movement adjustment
    [SerializeField]
    Vector2 XZLookDirection;

    //get active object type
    private bool floorObject;
    private bool wallObject;
    private bool ceilingObject;

    //look direction variables
    private bool lookForward;
    private bool lookRight;
    private bool lookLeft;
    private bool lookBack;

    private void Start()
    {
        _axisAdjusterScript = AxisAdjuster.GetComponent<MovementAdjuster>();
    }
    void Update()
    {
        FindActiveObject();
        
        SetInteractionLayer();

        FindLookDirection();
    }

    //Get the look direction X and Z factors
    void FindLookDirection()
    {

        lookForward = _axisAdjusterScript.forwardLook;
        lookRight = _axisAdjusterScript.rightLook;
        lookLeft = _axisAdjusterScript.leftLook;
        lookBack = _axisAdjusterScript.backwardLook;
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

            //Check to see if the active object is on wall/floor/ceiling
            FindActiveObjectConstraints();

        }
        catch
        {
            _activeGameObject = null;    
        }      
    }

    void FindActiveObjectConstraints()
    {
        floorObject = _activeGameObject.transform.GetComponentInParent<ObjectAffordances>().floorObject;
        wallObject = _activeGameObject.transform.GetComponentInParent<ObjectAffordances>().wallObject;
        ceilingObject = _activeGameObject.transform.GetComponentInParent<ObjectAffordances>().ceilingObject;
    }

    //Set the Interaction Layer based on the UI button selected
    void SetInteractionLayer()
    {
        //If ActiveObjectManager gameobject is active in the scene, try to get the layer int
        try
        {
            if (gameObject.layer == 0)
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
                _interactionLayer = gameObject.layer;

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


    //To show what interaction is currently active in the scene
    void UpdateInteractioIndicator(string indicator)
    {
        interactionIndicator = GameObject.Find("interactionIndicator");
        interactionIndicator.GetComponent<TMPro.TextMeshProUGUI>().text = indicator;
    }

    //To determine the interaction type based on the layer number
    public void DetermineInteraction(int layer)
    {
        switch(layer)
        {
            case 0://Default
                Debug.Log("No interaction is selected despite having an active object");
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



        if (floorObject || ceilingObject)
        {
            
            if(lookForward)//Look Z+
            {
                obj.transform.Translate(new Vector3(position.x, 0, position.y) * Time.deltaTime * speed, Space.World);
            }

            
            else if (lookLeft)//Look X-
            {
                obj.transform.Translate(new Vector3(-position.y, 0, position.x) * Time.deltaTime * speed, Space.World);
            }

            
            else if (lookBack)//Look Z-
            {
                obj.transform.Translate(new Vector3(-position.x, 0, -position.y) * Time.deltaTime * speed, Space.World);
            }

            
            else if (lookRight)//Look X+
            {
                obj.transform.Translate(new Vector3(position.y, 0, -position.x) * Time.deltaTime * speed, Space.World);
            }
        }

        //if the selected Object is a wall object
        else if (wallObject)
        {
            //If the wall is On the ZY plane
            if (_activeGameObject.GetComponent<SetXPositionFix>() != null)
            {
                var positionScript = _activeGameObject.GetComponent<SetXPositionFix>();

                Debug.Log("It is a ZY wall!!!!!!!!");
                //Check the wall position
                if (positionScript.leftWall )
                {
                    obj.transform.Translate(new Vector3(0, position.y, position.x) * Time.deltaTime * speed, Space.World);
                }
                else if (positionScript.rightWall)
                {
                    obj.transform.Translate(new Vector3(0, position.y, -position.x) * Time.deltaTime * speed, Space.World);
                }
            }

            //If the wall is On the XY plane
            else
            {
                var positionScript = _activeGameObject.GetComponent<SetZPositionFix>();
                Debug.Log("It is a XY wall!!!!!!!!");
                //check the wall position
                if (positionScript.frontWall)
                {
                    obj.transform.Translate(new Vector3(position.x, position.y, 0) * Time.deltaTime * speed, Space.World);
                }
                else if (positionScript.backWall)
                {
                    obj.transform.Translate(new Vector3(-position.x, position.y, 0) * Time.deltaTime * speed, Space.World);
                }

            }

            
        }
        
    }


    //To rotate the object around its own Y-Axis(Objects on the floor)
    void DoObjectRotate(GameObject obj, Vector2 position)
    {
        //If the active object is a floor or ceiling object, rotate it around Y axis
        if (floorObject || ceilingObject)
        {
            obj.transform.RotateAround(obj.transform.position, new Vector3(0, position.x, 0), 45 * Time.deltaTime);
        }

        //If the active object is a wall object check to see what kind of wall it is located on 
        else if (wallObject)
        {
            //If the wall is On the ZY plane
            if (_activeGameObject.GetComponent<SetXPositionFix>() != null)
            {
                Debug.Log("It is a ZY wall!!!!!!!!");

                var positionScript = _activeGameObject.GetComponent<SetXPositionFix>();
                if (positionScript.rightWall)
                {
                    obj.transform.RotateAround(obj.transform.position, new Vector3(-position.x, 0, 0), 45 * Time.deltaTime);

                }
                else
                {
                    obj.transform.RotateAround(obj.transform.position, new Vector3(position.x, 0, 0), 45 * Time.deltaTime);
                }

            }

            else
            {
                //If the wall is on the XY plane
                Debug.Log("It is a XY wall!!!!!!!!");

                var positionScript = _activeGameObject.GetComponent<SetZPositionFix>();
                if (positionScript.frontWall)
                {
                    obj.transform.RotateAround(obj.transform.position, new Vector3(0, 0, -position.x), 45 * Time.deltaTime);
                }
                else if (positionScript.backWall)
                {
                    obj.transform.RotateAround(obj.transform.position, new Vector3(0, 0, position.x), 45 * Time.deltaTime);

                }
            }
            
            
                
        }
            
    }


    void DoObjectDrag(GameObject obj)
    {
        
    }
}

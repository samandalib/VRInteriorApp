using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IndicatorCanvas : MonoBehaviour
{
    public Transform targetCamera;

    public GameObject activeObjectManager;

    private GameObject _activeObject;
    private Vector3 _activeObjectPosition;
    private Vector3 _activeObjectScale;

    [SerializeField]
    private ObjectAffordances activeObjectType;

    public bool floorPlane;
    public bool ceilingPlane;
    public bool rightWallPlane;
    public bool leftWallPlane;
    public bool frontWallPlane;
    public bool backWallPlane; 

    private Transform activeObjectPlane;
    private Vector3 PlaneRotation;

    private Vector3 _cameraRotation;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
        //Check to see if there is an active object and if there is, see what plane that object belongs to
        try
        {
            //_activeObject = GameObject.FindGameObjectsWithTag("ActiveObject")[0];
            _activeObject = activeObjectManager.GetComponent<ActiveObjectManager>().activeGameObject;
            Transform _activeObjectParent = _activeObject.transform.parent;
            float prefabBoxColliderHeight = _activeObjectParent.GetComponent<ActiveObjectDependancies>().Prefab.GetComponent<BoxCollider>().size.y;
            _activeObjectPosition = _activeObject.transform.position;
            _activeObjectScale = _activeObject.transform.lossyScale;


            activeObjectType = _activeObject.GetComponentInParent<ObjectAffordances>();
            activeObjectPlane = _activeObject.transform.parent.transform.parent;

            //Quaternion PlaneRotation = activeObjectPlane.rotation;///THIS DOES NOT RETURN THE ROTATION VALUES FROM INSPECTOR!!!!!
            #if UNITY_EDITOR
                 PlaneRotation = UnityEditor.TransformUtils.GetInspectorRotation(activeObjectPlane.transform);//THIS ONE DOES RETURN THE VALUES FROM INSPECTOR
            #endif

            if (PlaneRotation.x == 0 && PlaneRotation.z == 0)
            {
                floorPlane = true;
                ceilingPlane = rightWallPlane = leftWallPlane = frontWallPlane = backWallPlane = false;
                //Rotate the canvas content based with regard to camera position and rotation
                transform.position = new Vector3(_activeObjectPosition.x, prefabBoxColliderHeight, _activeObjectPosition.z);

                RotateCanvas("floor");
            }
            else if (PlaneRotation.x == 0 && Math.Abs(PlaneRotation.z) == 180)
            {
                ceilingPlane = true;
                floorPlane = rightWallPlane = leftWallPlane = frontWallPlane = backWallPlane = false;
                transform.position = new Vector3(_activeObjectPosition.x, _activeObjectPosition.y-_activeObjectScale.y, _activeObjectPosition.z );
                RotateCanvas("ceiling");
            }
            else if (PlaneRotation.z == 90)
            {
                rightWallPlane = true;
                floorPlane = ceilingPlane = leftWallPlane = frontWallPlane = backWallPlane = false;
                RotateCanvas("rightwall");
                transform.position = new Vector3(_activeObjectPosition.x - _activeObjectScale.y, _activeObjectPosition.y, _activeObjectPosition.z);
            }
            else if (PlaneRotation.z == -90)
            {
                leftWallPlane = true;
                floorPlane = ceilingPlane = rightWallPlane = frontWallPlane = backWallPlane = false;
                RotateCanvas("leftwall");
                transform.position = new Vector3(_activeObjectPosition.x + _activeObjectScale.y, _activeObjectPosition.y, _activeObjectPosition.z);
            }
            else if (PlaneRotation.x == -90)
            {
                frontWallPlane = true;
                floorPlane = ceilingPlane = rightWallPlane = leftWallPlane = backWallPlane = false;
                RotateCanvas("frontwall");
                transform.position = new Vector3(_activeObjectPosition.x, _activeObjectPosition.y, _activeObjectPosition.z - _activeObjectScale.y);
            }
            else if (PlaneRotation.x == 90)
            {
                backWallPlane = true;
                floorPlane = ceilingPlane = rightWallPlane = leftWallPlane = frontWallPlane = false;
                RotateCanvas("backwall");
                transform.position = new Vector3(_activeObjectPosition.x , _activeObjectPosition.y, _activeObjectPosition.z + _activeObjectScale.y);
            }
        }
        catch
        {
            Debug.Log("No active Object is available to get ObjectAffordance Information from");
        }
    }

    void RotateCanvas(string location)
    {
        #if UNITY_EDITOR
            _cameraRotation = UnityEditor.TransformUtils.GetInspectorRotation(targetCamera);
        #endif
        Vector3 _angles = transform.eulerAngles;

        switch (location)
        {
            case "floor":
                transform.LookAt(targetCamera,Vector3.up);
                transform.Rotate(0, 180, 0);
                break;
            case "ceiling":
                //transform.eulerAngles = new Vector3(90, 0, 0);
                transform.LookAt(targetCamera, Vector3.down);
                transform.Rotate(0, 180, 180);
                break;
            case "rightwall":
                transform.LookAt(targetCamera);
                transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            case "leftwall":
                transform.LookAt(targetCamera);
                transform.eulerAngles = new Vector3(0,-90, 0);
                break;
            case "frontwall":
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case "backwall":
                transform.eulerAngles = new Vector3(0, 180, 0);
                break;
        }

    }

    //This method sets the size of the Interaction indicator based on the distance from active object
    public void UpdateSize(float deltaPosition)
    {
        float baseFontSize = 4.0f;
        float maxFontSize = 24.0f;
        float changeThreshold = 3.0f;
        float stopChange = 12.0f;


        if (deltaPosition < changeThreshold)
        {
            transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().fontSize = baseFontSize;
        }

        else if (deltaPosition > stopChange)
        {
            transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().fontSize = maxFontSize;
        }

        else
        {
            transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().fontSize = baseFontSize * (deltaPosition / changeThreshold);
        }
    }
    
}

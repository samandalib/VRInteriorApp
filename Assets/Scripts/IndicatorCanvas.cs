using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorCanvas : MonoBehaviour
{
    public Transform targetCamera;

    [SerializeField]
    private GameObject _activeObject;

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
            _activeObject = GameObject.FindGameObjectsWithTag("ActiveObject")[0];
            activeObjectType = _activeObject.GetComponentInParent<ObjectAffordances>();
            activeObjectPlane = _activeObject.transform.parent.transform.parent;
            //Quaternion PlaneRotation = activeObjectPlane.rotation;///THIS DOES NOT RETURN THE ROTATION VALUES FROM INSPECTOR!!!!!

            PlaneRotation = UnityEditor.TransformUtils.GetInspectorRotation(activeObjectPlane.transform);//THIS ONE DOES RETURN THE VALUES FROM INSPECTOR

            if (PlaneRotation.x == 0 && PlaneRotation.z == 0)
            {
                floorPlane = true;
                ceilingPlane = rightWallPlane = leftWallPlane = frontWallPlane = backWallPlane = false;
                //Rotate the canvas content based with regard to camera position and rotation
                
                RotateCanvas("floor");
            }
            else if (PlaneRotation.x == 0 && Math.Abs(PlaneRotation.z) == 180)
            {
                ceilingPlane = true;
                floorPlane = rightWallPlane = leftWallPlane = frontWallPlane = backWallPlane = false;
                
                RotateCanvas("ceiling");
            }
            else if (PlaneRotation.z == 90)
            {
                rightWallPlane = true;
                floorPlane = ceilingPlane = leftWallPlane = frontWallPlane = backWallPlane = false;
                RotateCanvas("rightwall");
            }
            else if (PlaneRotation.z == -90)
            {
                leftWallPlane = true;
                floorPlane = ceilingPlane = rightWallPlane = frontWallPlane = backWallPlane = false;
                RotateCanvas("leftwall");
            }
            else if (PlaneRotation.x == -90)
            {
                frontWallPlane = true;
                floorPlane = ceilingPlane = rightWallPlane = leftWallPlane = backWallPlane = false;
                RotateCanvas("frontwall");
            }
            else if (PlaneRotation.x == 90)
            {
                backWallPlane = true;
                floorPlane = ceilingPlane = rightWallPlane = leftWallPlane = frontWallPlane = false;
                RotateCanvas("backwall");
            }

            

        }
        catch
        {
            Debug.Log("No active Object is available to get ObjectAffordance Information from");
        }
    }

    void RotateCanvas(string location)
    {
        _cameraRotation = UnityEditor.TransformUtils.GetInspectorRotation(targetCamera);

        Vector3 _angles = transform.eulerAngles;

        switch (location)
        {
            case "floor":
                transform.LookAt(targetCamera,Vector3.up);
                transform.Rotate(0, 180, 0);
                break;
            case "ceiling":
                transform.eulerAngles = new Vector3(90, 0, 0);
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
        float baseFontSize = 12.0f;
        float maxFontSize = 48.0f;
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

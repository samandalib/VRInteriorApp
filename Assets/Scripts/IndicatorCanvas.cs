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

    bool floorPlane;
    bool ceilingPlane;
    bool rightWallPlane;
    bool leftWallPlane;
    bool frontWallPlane;
    bool backWallPlane; 

    private Transform activeObjectPlane;


    // Update is called once per frame
    void Update()
    {
        //Check to see if there is an active object and if there is, see what plane that object belongs to
        try
        {
            _activeObject = GameObject.FindGameObjectsWithTag("activeObject")[0];

            activeObjectType = _activeObject.GetComponentInParent<ObjectAffordances>();

            activeObjectPlane = _activeObject.transform.parent.transform.parent;

            Quaternion PlaneRotation = activeObjectPlane.transform.rotation;

            if (PlaneRotation.x == 0 && PlaneRotation.y == 0 && PlaneRotation.z == 0)
            {
                floorPlane = true;
                ceilingPlane = rightWallPlane = leftWallPlane = frontWallPlane = backWallPlane = false;
            }
            else if (PlaneRotation.x == 0 && PlaneRotation.y == 0 && PlaneRotation.z == 180)
            {
                ceilingPlane = true;
                floorPlane = rightWallPlane = leftWallPlane = frontWallPlane = backWallPlane = false;
            }
            else if (PlaneRotation.x == 0 && PlaneRotation.y == 0 && PlaneRotation.z == 90)
            {
                rightWallPlane = true;
                floorPlane = ceilingPlane = leftWallPlane = frontWallPlane = backWallPlane = false;
            }
            else if (PlaneRotation.x == 0 && PlaneRotation.y == 180 && PlaneRotation.z == 90)
            {
                leftWallPlane = true;
                floorPlane = ceilingPlane = rightWallPlane = frontWallPlane = backWallPlane = false;
            }
            else if (PlaneRotation.x == 0 && PlaneRotation.y == -90 && PlaneRotation.z == 90)
            {
                frontWallPlane = true;
                floorPlane = ceilingPlane = rightWallPlane = leftWallPlane = backWallPlane = false;
            }
            else if (PlaneRotation.x == 0 && PlaneRotation.y == 90 && PlaneRotation.z == 90)
            {
                backWallPlane = true;
                floorPlane = ceilingPlane = rightWallPlane = leftWallPlane = frontWallPlane = false;
            }

            //Rotate the canvas content based with regard to camera position and rotation
            RotateCanvas();

        }
        catch
        {
            Debug.Log("No active Object is available to get ObjectAffordance Information from");
        }
    }

    void RotateCanvas()
    {
        Quaternion cameraRotation = targetCamera.transform.rotation;
        if (activeObjectType.floorObject)
        {
            transform.LookAt(targetCamera);
            transform.rotation = targetCamera.transform.rotation;
        }
        else if (activeObjectType.wallObject)
        {
            transform.rotation = targetCamera.transform.rotation;
        }
        else if (activeObjectType.ceilingObject)
        {
            transform.rotation = new Quaternion(90.0f, cameraRotation.y, cameraRotation.z, cameraRotation.w);
        }
    }
}

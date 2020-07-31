using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInteraction : MonoBehaviour
{
    private ActiveObjectManager _manager;

    public GameObject AxisAdjuster;
    private MovementAdjuster _axisAdjuster;

    [SerializeField]
    private GameObject activeObject;


    // Start is called before the first frame update
    void Start()
    {
        _manager = transform.GetComponent<ActiveObjectManager>();
        _axisAdjuster = AxisAdjuster.GetComponent<MovementAdjuster>();

    }

    // Update is called once per frame
    void Update()
    {
        activeObject = _manager.activeGameObject;
    }

    //To rotate the object around its own Y-Axis(Objects on the floor)
    public void DoObjectRotate(GameObject obj, Vector2 position)
    {
        try
        {
            //If the active object is a floor or ceiling object, rotate it around Y axis
            if (_manager.floorObject || _manager.ceilingObject)
            {
                obj.transform.RotateAround(obj.transform.position, new Vector3(0, position.x, 0), 45 * Time.deltaTime);
            }

            //If the active object is a wall object check to see what kind of wall it is located on 
            else if (_manager.wallObject)
            {
                //If the wall is On the ZY plane
                if (activeObject.GetComponent<SetXPositionFix>() != null)
                {
                    Debug.Log("It is a ZY wall!!!!!!!!");

                    var positionScript = activeObject.GetComponent<SetXPositionFix>();
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

                    var positionScript = activeObject.GetComponent<SetZPositionFix>();
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
        catch
        {
            Debug.Log("Error Catch from RotateInteraction");
        }


    }
}

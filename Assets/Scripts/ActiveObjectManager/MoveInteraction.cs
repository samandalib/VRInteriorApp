using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInteraction : MonoBehaviour
{
    private ActiveObjectManager _manager;

    public GameObject AxisAdjuster;
    private MovementAdjuster _axisAdjuster;

    public int speed;

    [SerializeField]
    private GameObject activeObject;

    private void Start()
    {
        _manager = transform.GetComponent<ActiveObjectManager>();
        _axisAdjuster = AxisAdjuster.GetComponent<MovementAdjuster>();

        speed = _manager.movementSpeed;


    }

    private void Update()
    {
        activeObject = _manager.activeGameObject;
        
    }
    //Function for moving item on the floor (X-Z plane) using 2DAxis input
    public void DoObjectMove(GameObject obj, Vector2 position)
    {

        if (_manager.floorObject || _manager.ceilingObject)
        {

            if (_axisAdjuster.forwardLook)//Look Z+
            {
                obj.transform.Translate(new Vector3(position.x, 0, position.y) * Time.deltaTime * speed, Space.World);
            }


            else if (_axisAdjuster.leftLook)//Look X-
            {
                obj.transform.Translate(new Vector3(-position.y, 0, position.x) * Time.deltaTime * speed, Space.World);
            }


            else if (_axisAdjuster.backwardLook)//Look Z-
            {
                obj.transform.Translate(new Vector3(-position.x, 0, -position.y) * Time.deltaTime * speed, Space.World);
            }


            else if (_axisAdjuster.rightLook)//Look X+
            {
                obj.transform.Translate(new Vector3(position.y, 0, -position.x) * Time.deltaTime * speed, Space.World);
            }
        }

        //if the selected Object is a wall object
        else if (_manager.wallObject)
        {
            //If the wall is On the ZY plane
            if (_manager.activeGameObject.GetComponent<SetXPositionFix>() != null)
            {
                var positionScript = activeObject.GetComponent<SetXPositionFix>();

                Debug.Log("It is a ZY wall!!!!!!!!");
                //Check the wall position
                if (positionScript.leftWall)
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
                var positionScript = activeObject.GetComponent<SetZPositionFix>();
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
}

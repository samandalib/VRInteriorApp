using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetZPositionFix : MonoBehaviour
{
    public GameObject Plane;
    private Vector3 PlanePos;

    public bool frontWall;
    public bool backWall;

    void Start()
    {
        //Need the Plane Position for the adjustment of Target position on the plane
        PlanePos = Plane.transform.position;
    }


    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = Plane.transform.position.z;//the Target should maintain its X position to where the Plane is

        //var zAdj = transform.localScale.z / 2;//Need this to adjust the position of the target

        if (PlanePos.z > 0)//If the Plane is located in front (positive Z) side of the environment
        {
            frontWall = true;
            backWall = false;
            //transform.position = new Vector3(x , y, z - zAdj);
            transform.position = new Vector3(x , y, z);
        }
        else if (PlanePos.z < 0)//If the Plane is located on the back (negative Z) side of the environment
        {
            frontWall = false;
            backWall = true;
            //transform.position = new Vector3(x , y, z+zAdj);
            transform.position = new Vector3(x , y, z);
        }
    }
}

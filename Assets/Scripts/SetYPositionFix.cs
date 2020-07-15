using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetYPositionFix : MonoBehaviour
{
    public GameObject Plane;
    private Vector3 PlanePos;

    void Start()
    {

        //Need the Plane Position for the adjustment of Target position on the plane
        PlanePos = Plane.transform.position;
    }


    void Update()
    {
        float x = transform.position.x;
        float y = Plane.transform.position.y;//the Target should maintain its Y position to where the Plane is
        float z = transform.position.z;

        var yAdj = transform.localScale.y / 2;//Need this to adjust the position of the target

        if (PlanePos.y > 0)//If the Plane is located on the top (positive Y) side of the environment
        {
            transform.position = new Vector3(x, y-yAdj, z);

        }
        else if (PlanePos.y <= 0)//If the Plane is located on the down (negative y) side of the environment
        {
            transform.position = new Vector3(x, y+yAdj, z);

        }
    }
}

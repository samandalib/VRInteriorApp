﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetXPositionFix : MonoBehaviour
{

    public GameObject Plane;
    private Vector3 PlanePos;

    public bool rightWall;
    public bool leftWall;

    void Start()
    {

        //Need the Plane Position for the adjustment of Target position on the plane
        PlanePos = Plane.transform.position;

    }

   
    void Update()
    {
        float x = Plane.transform.position.x;//the Target should maintain its X position to where the Plane is
        float y = transform.position.y;
        float z = transform.position.z;

        //var xAdj = transform.localScale.x / 2;//Need this to adjust the position of the target

        if (PlanePos.x>0)//If the Plane is located on the right (positive X) side of the environment
        {
            rightWall = true;
            leftWall = false;
            //transform.position = new Vector3(x - xAdj, y, z);
            transform.position = new Vector3(x, y, z);

        }
        else if (PlanePos.x<0)//If the Plane is located on the left (negative X) side of the environment
        {
            leftWall = true;
            rightWall = false;
            //transform.position = new Vector3(x + xAdj, y, z);
            transform.position = new Vector3(x, y, z);

        }
    }
}

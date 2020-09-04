using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAdjuster : MonoBehaviour
{
    public GameObject pointFar;
    public GameObject pointClose;

    public Vector3 lookDirection;

    public bool forwardLook;
    public bool backwardLook;
    public bool rightLook;
    public bool leftLook;

    private double _threshold;
    // Start is called before the first frame update
    void Start()
    {
        //The feild of veiw for each direction is considered 90 degrees on a perfect circle
        //the threshold is sqrt(2)/2 of the lookArrow magnitude
        _threshold = Mathf.Sqrt(2.0f)/2;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pointFarPos = pointFar.transform.position;
        Vector3 pointClosePos = pointClose.transform.position;

        lookDirection = (pointFarPos - pointClosePos);

        //we only need the X and Z value of the lookDirection to find the lookArrow Vector2
        Vector2 lookArrow = new Vector2(lookDirection.x, lookDirection.z).normalized;


        float x = lookArrow.x;
        float z = lookArrow.y;


        if (x > -_threshold && x < _threshold && z >= _threshold)//Look forward
        {
            forwardLook = true;
            leftLook = rightLook = backwardLook = false;
        }


        else if (z > -_threshold && z < _threshold && x <= -_threshold)//Look Left
        {
            leftLook = true;
            forwardLook = rightLook = backwardLook = false;
        }


        else if (x > -_threshold && x < _threshold && z <= -_threshold)//Look Backward
        {
            backwardLook = true;
            leftLook = rightLook = forwardLook = false;
        }

        else if (z > -_threshold && z < _threshold && x >= _threshold)//Look Right
        {
            rightLook = true;
            leftLook = forwardLook = backwardLook = false;
        }
        else
        {
            Debug.Log("Look Nowhere"+lookDirection);
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script changes the movement direction stem from Thumbstick based on the direction of player's view
public class AxisAdjuster : MonoBehaviour
{
    public GameObject axisAdjuster;
    private MovementAdjuster _axisAdjusterScript;

    //look direction variables
    public bool lookForward;
    public bool lookRight;
    public bool lookLeft;
    public bool lookBack;

    // Start is called before the first frame update
    void Start()
    {
        _axisAdjusterScript = axisAdjuster.GetComponent<MovementAdjuster>();
    }

    // Update is called once per frame
    void Update()
    {
        FindLookDirection();
    }

    void FindLookDirection()
    {

        lookForward = _axisAdjusterScript.forwardLook;
        lookRight = _axisAdjusterScript.rightLook;
        lookLeft = _axisAdjusterScript.leftLook;
        lookBack = _axisAdjusterScript.backwardLook;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAdjuster : MonoBehaviour
{
    public GameObject pointFar;
    public GameObject pointClose;

    
    public Vector3 lookDirection; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pointFarPos = pointFar.transform.position;
        Vector3 pointClosePos = pointClose.transform.position;

        lookDirection = (pointFarPos - pointClosePos).normalized;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransformToZero : MonoBehaviour
{
    
    
    void Update()
    {
        Vector3 parentPosition = transform.parent.position;
        transform.localPosition = new Vector3(parentPosition.x, 1.3f, parentPosition.z );
    }
}

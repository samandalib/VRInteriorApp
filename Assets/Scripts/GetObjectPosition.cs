using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objectPos = transform.position;
        Debug.Log("Object position is now: " + objectPos);
    }
}

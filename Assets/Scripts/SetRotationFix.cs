using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotationFix : MonoBehaviour
{

    private Quaternion FirstRotation;
    // Start is called before the first frame update
    void Start()
    {
        //need FirtsRotation to maintain the rotation of the object through the transformation process
        FirstRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = FirstRotation;
    }
}

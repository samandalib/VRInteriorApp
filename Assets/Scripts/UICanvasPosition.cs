using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasPosition : MonoBehaviour
{

    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 3.0f);
        transform.localRotation = new Quaternion(0, 0, 0, 0);
    }
}

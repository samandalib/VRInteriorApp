﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransformToZero : MonoBehaviour
{

    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);      

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransformToZero : MonoBehaviour
{
    //GameObject _target;

    void Update()
    {
        //Find the height of the prefab in the active object
        //_target = GameObject.FindGameObjectsWithTag("ActiveObject")[0];

        transform.localPosition = new Vector3(0, 0, 0);
        /*
        float _targetHeight = _target.transform.GetChild(0).localScale.y;


        if (_targetHeight < 1.3f)
        {
            transform.localPosition = new Vector3(0, 1.3f, 0);
        }
        else
        {
            float indicatorPlacement = _targetHeight / 2 + 0.3f;
            transform.localPosition = new Vector3(0, indicatorPlacement, 0);
        }
            
        */
        

    }
}

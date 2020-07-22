using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAffordances : MonoBehaviour
{
    public bool floorObject;
    public bool wallObject;
    public bool ceilingObject;

    public bool rotation;
    public bool move;
    public bool material;
    public bool scale;
    public bool drag;
    
    // Start is called before the first frame update
    void Start()
    {
        if (floorObject)
        {
            rotation = true;
            move = true;
            material = true;
            drag = true;
        }
        else if (wallObject)
        {
            move = true;
            material = true;
        }
        else if (ceilingObject)
        {
            move = true;
            material = true;
            rotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

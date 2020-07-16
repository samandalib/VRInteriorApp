using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChildObject : MonoBehaviour
{
    Transform child;
    Color BaseColor;
    Color HoverColor = Color.yellow;
    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0);
        Debug.Log("Child is Found ......" + child + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        BaseColor = child.GetComponent<MeshRenderer>().material.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverEnter()
    {
        child.GetComponent<MeshRenderer>().material.color = HoverColor;
    }

    public void OnHoverExit()
    {
        child.GetComponent<MeshRenderer>().material.color = BaseColor;
    }
}

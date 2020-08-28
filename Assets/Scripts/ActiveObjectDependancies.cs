using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This file works as a facade for setting up the interactive object to call Canvase and the functions of the ActiveObjectManager
public class ActiveObjectDependancies : MonoBehaviour
{
    
    public GameObject ActiveObjectManager;
    public GameObject TargetObject;
    public GameObject Prefab;
    public Light[] lights;

    private Color _baseColor;

    private void Awake()
    {
        _baseColor = Prefab.GetComponent<MeshRenderer>().material.color;
    }
    


    //Set to the original color of the prefab when hover exits the object
    public void SetToBaseColor()
    {
        Prefab.GetComponent<MeshRenderer>().material.color = _baseColor;
    }

    //Set the prefab color to yellow when hover enters the object
    public void SetToHoverColor()
    {
        Prefab.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
}

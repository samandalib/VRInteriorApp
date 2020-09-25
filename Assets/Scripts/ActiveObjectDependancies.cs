using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This file works as a facade for setting up the interactive object to call Canvase and the functions of the ActiveObjectManager

//IMPORTANT: THIS SCRIPT DOESN'T WORK ON BUILD, ONLY WORKS WELL ON UNITY EDITOR
public class ActiveObjectDependancies : MonoBehaviour
{
    
    //public GameObject ActiveObjectManager;
    public GameObject statusShow;
    //public GameObject TargetObject;
    public GameObject Prefab;
    public Light[] lights;


    public static Color _baseColor;
    //public static Material _baseMaterial;

    [SerializeField]
    private Color baseColor;

    private void Awake()
    {
        
        try
        {
            _baseColor = Prefab.GetComponent<MeshRenderer>().material.color;
        }
        catch
        {
            Debug.Log("There is no MeshRenderer/color available for the prefab");
        }
        baseColor = _baseColor;
    }
    


    //Set to the original color of the prefab when hover exits the object
    public void SetToBaseColor()
    {
        
        /*
        try
        {
            Prefab.GetComponent<MeshRenderer>().material.color = _baseColor;
            Prefab.GetComponent<MeshRenderer>().materials[0] = _baseMaterial;
        }
        catch
        {
            Debug.Log("There is no MeshRenderer available for the prefab");
        }
        */

    }

    //Set the prefab color to yellow when hover enters the object
    public void SetToHoverColor()
    {
        /*
        try
        {
            if (statusShow.activeInHierarchy)
            {
                try
                {
                    Prefab.GetComponent<MeshRenderer>().material.color = _baseColor;
                }
                catch
                {
                    Debug.Log("cannot make hover color for this object");
                }
            }

            else
            {

                try
                {
                    Prefab.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }
                catch
                {
                    Debug.Log("cannot make hover color for this object");
                }
            }
        }
        catch
        {
            Debug.Log("There is no MeshRenderer available for the prefab");
        }
        */
    }
}

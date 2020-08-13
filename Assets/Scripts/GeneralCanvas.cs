using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GeneralCanvas : MonoBehaviour
{
    public XRController controller;
    public GameObject targetCanvas;

    //see if the 
    public GameObject[] overlappingCanvases;
    public Transform parentCamera;

    private bool _activeOverlappingCanvas;

    public bool buttonStatus;


    // Update is called once per frame
    void Update()
    {
        //see if there is any active canvas in the scene that might overlap with the menu canvas if active
        for (int i = 0; i < overlappingCanvases.Length; i++)
        {
            if (overlappingCanvases[i].activeInHierarchy)
            {
                _activeOverlappingCanvas = true;
            }
            else
            {
                _activeOverlappingCanvas = false;
            }
        }

        //Check to see if there is any Input from the assigned controller
        CheckForInput();

        //if the menu button is pressed and there is no overlapping canvas in the scene, set the menu active and make it a child of camera
        if (buttonStatus && !_activeOverlappingCanvas)
        {
            targetCanvas.transform.SetParent(parentCamera);
            targetCanvas.SetActive(true);

        }
        else
        {
            targetCanvas.SetActive(false);
        }

    }

    //Check if there is any input from controller
    public void CheckForInput()
    {
        if (controller.enableInputActions)
            CheckForInput(controller.inputDevice);
    }
    //See if the input is from MenuButton of the assigned controller
    private void CheckForInput(InputDevice device)
    {
        if (device.TryGetFeatureValue(CommonUsages.menuButton, out buttonStatus))
            Debug.Log("Menu Button Status: " + buttonStatus);
    }
}

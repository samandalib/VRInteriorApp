using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GeneralCanvas : MonoBehaviour
{
    public XRController controller;
    public GameObject targetCanvas;
    private Transform _targetBaseParent;

    //see if the 
    public GameObject[] overlappingCanvases;
    public Transform parentCamera;

    private bool _activeOverlappingCanvas;

    public bool buttonStatus;
    private bool _buttonPressed;
    private bool _showCanvas;

    private void Start()
    {
        _targetBaseParent = targetCanvas.transform.parent;

        buttonStatus = false;

        _showCanvas = false;

        //
        StartCoroutine(WaitSomeTimeAndUpdate());
    }

    private void Update()
    {
        ShowCanvas(_showCanvas);
    }

    //Method to see when is the time to show the canvas
    void ShowCanvas(bool showCanvas)
    {
        if (showCanvas && !_activeOverlappingCanvas)
        {
            targetCanvas.transform.SetParent(parentCamera);
            targetCanvas.SetActive(true);
        }
        else
        {
            targetCanvas.transform.SetParent(_targetBaseParent);
            targetCanvas.SetActive(false);
        }
    }


    // To make the input more controllable, the update rate is manually set to 2 times/second instead of using Unity Update method
    IEnumerator WaitSomeTimeAndUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

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

            if (buttonStatus && !_showCanvas)
            {
                _showCanvas = true;

                Debug.Log("Show Canvas Value is true: " + _showCanvas);

            }
            else if (buttonStatus && _showCanvas)
            {
                _showCanvas = false;
                Debug.Log("Show Canvas Value is false: " + _showCanvas);
            }

            //ShowCanvas(_showCanvas);
            Update();
            //Debug.Log("Show Canvas>>>>>>>>>>>>>>>>>>>>>>>>>>");
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

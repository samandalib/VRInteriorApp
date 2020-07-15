using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectionRayController : MonoBehaviour
{
    public XRController selectionRayController;
    public InputHelpers.Button selectionActivationButton;
    public float activationThreshold;

    // Update is called once per frame
    void Update()
    {
        if (selectionRayController)
        {
            selectionRayController.gameObject.SetActive(CheckIfActivated(selectionRayController));
        }
    }

    public bool CheckIfActivated (XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, selectionActivationButton, out bool isActivated, activationThreshold);
        return isActivated;

    }
}

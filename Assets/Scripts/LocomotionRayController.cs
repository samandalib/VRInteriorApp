using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionRayController : MonoBehaviour
{
    public XRController teleportRayController;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold;

    void Update()
    {
        if (teleportRayController)
        {
            teleportRayController.gameObject.SetActive(CheckIfActivated(teleportRayController));
        }
    }

    public bool CheckIfActivated (XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}

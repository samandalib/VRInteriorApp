﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetermineInteraction : MonoBehaviour
{

    private ActiveObjectManager _manager;


    private MoveInteraction _doMove;
    private RotateInteraction _doRotate;

    [SerializeField]
    private int _interactionLayer;
    public string interactionType;

    private GameObject _rig;
    private GameObject _activeGameObject;

    //[SerializeField]
    private GameObject interactionIndicator;


    // Start is called before the first frame update

    void Start()
    {
        _rig = transform.GetComponent<ActiveObjectManager>().Rig;
        _manager = transform.GetComponent<ActiveObjectManager>();

    }

    // Update is called once per frame
    void Update()
    {
        _activeGameObject = _manager.activeGameObject;

        _interactionLayer = transform.GetComponent<InteractionLayer>().interactionLayer;
        Debug.Log("interaction Layer from DetermineInteraction Script: " + _interactionLayer);

        DetermineInteractionType(_interactionLayer);

        _doMove = transform.GetComponent<MoveInteraction>();
        _doRotate = transform.GetComponent<RotateInteraction>();
    }

    public void DetermineInteractionType(int layer)
    {
        try
        {
            switch (layer)
            {
                case 0://Default
                    Debug.Log("No interaction is selected despite having an active object");
                    interactionType = "";
                    UpdateInteractioIndicator(interactionType);

                    break;

                case 11://rotate
                    interactionType = "Rotate";
                    UpdateInteractioIndicator(interactionType);
                    Debug.Log("Rotation Is selected !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    _activeGameObject.GetComponent<SetRotationFix>().enabled = false;
                    _manager.CheckForInput();
                    _doRotate.DoObjectRotate(_activeGameObject, _manager.newPosition);
                    break;

                case 12://move
                        //Debug.Log("MOVE Is selected ");
                    interactionType = "Move";
                    UpdateInteractioIndicator(interactionType);
                    //Rig.GetComponent<Locomotion2DAxis>().controllers[0] = null;
                    _manager.CheckForInput();
                    _doMove.DoObjectMove(_activeGameObject, _manager.newPosition);
                    break;

                case 13://material
                    interactionType = "Material";
                    UpdateInteractioIndicator(interactionType);
                    Debug.Log("MATERIAL Is selected !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    break;


            }
        }
        catch
        {
            Debug.Log("Error Catched from DetermineInteraction Script");
        }


    }

    //To show what interaction is currently active in the scene
    void UpdateInteractioIndicator(string indicator)
    {

        float baseFontSize = 18.0f;
        float maxFontSize = 56.0f;

        interactionIndicator = GameObject.Find("interactionIndicator");
        interactionIndicator.GetComponent<TMPro.TextMeshProUGUI>().text = indicator;

        Vector3 objectPosition = _activeGameObject.transform.position;
        Vector3 CameraPosition = _rig.transform.position;

        float deltaPosition = Vector3.Distance(objectPosition, CameraPosition);
        float changeThreshold = 3.0f;
        float stopChange = 12.0f;

        if (deltaPosition < changeThreshold)
        {
            interactionIndicator.GetComponent<TMPro.TextMeshProUGUI>().fontSize = baseFontSize;
        }

        else if (deltaPosition > stopChange)
        {
            interactionIndicator.GetComponent<TMPro.TextMeshProUGUI>().fontSize = maxFontSize;
        }

        else
        {
            interactionIndicator.GetComponent<TMPro.TextMeshProUGUI>().fontSize = baseFontSize * (deltaPosition / changeThreshold);
        }
    }
}

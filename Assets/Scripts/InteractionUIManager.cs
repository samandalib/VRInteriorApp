﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUIManager : MonoBehaviour
{
    public GameObject activeGameObject;
    private ActiveObjectManager _script;
    private bool _lightObject;
    public GameObject lightButton;

    // Start is called before the first frame update
    void Start()
    {
        _script = activeGameObject.GetComponent<ActiveObjectManager>();
        
    }


    void Update()
    {
        _lightObject = _script.activeGameObject.transform.parent.GetComponent<ObjectAffordances>().lightHolderObject;
        if (_lightObject)
        {
            lightButton.SetActive(true);
        }
        else
        {
            lightButton.SetActive(false);
        }
    }
}

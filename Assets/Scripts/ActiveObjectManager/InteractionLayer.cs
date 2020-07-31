using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionLayer : MonoBehaviour
{

    private DetermineInteraction interaction;
    public int interactionLayer;

    private ActiveObjectManager _manager;

    private GameObject _rig;


    // Start is called before the first frame update
    void Start()
    {
        _rig = transform.GetComponent<ActiveObjectManager>().Rig;
        interaction = transform.GetComponent<DetermineInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        SetInteractionLayer();
    }

    void SetInteractionLayer()
    {
        try
        {

            interactionLayer = gameObject.layer;

            //If ActiveObjectManager gameobject is active in the scene, try to get the layer int

            if (interactionLayer == 0)
            {

                //The 2DAxis on the controller will work for locomotion
                _rig.GetComponent<Locomotion2DAxis>().enabled = true;
                interaction.DetermineInteractionType(interactionLayer);
            }
            else
            {

                //locomotion will be disabled the 2DAxis on selected controller will work for object interaction
                _rig.GetComponent<Locomotion2DAxis>().enabled = false;
                interaction.DetermineInteractionType(interactionLayer);
            }
        }
        catch
        {
            Debug.LogError("Error from Set intertaction layer catched");
        }
    }
}

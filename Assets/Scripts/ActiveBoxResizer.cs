using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveBoxResizer : MonoBehaviour
{
    public GameObject activeObjectManager;
    public GameObject selectionBox;
    //public Canvas indicatorCanvas;

    [SerializeField]
    private readonly float boxMargin = 0.05f;

    [SerializeField]
    private GameObject _activeGameObject;
    [SerializeField]
    private GameObject _prefab;

    //private Vector3 canvasScale;
    private Vector3 _boxSize;
    private Vector3 _boxRotation;
    private Vector3 boxColliderSize;

    private Vector3 boxColliderCenter;

    private Vector3 colliderXScale;


    private Vector3 _boxPosition; 

    private Vector3 objectScale;

    private void Start()
    {
        //canvasScale = indicatorCanvas.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //indicatorCanvas.transform.localScale = canvasScale;
        try
        {
            //Get the Target object and the prefab
            _activeGameObject = activeObjectManager.GetComponent<ActiveObjectManager>().activeGameObject;
            _prefab = _activeGameObject.transform.parent.GetComponent<ActiveObjectDependancies>().Prefab;
            

            //Get boxCollider size and Target object scale
            boxColliderSize = _prefab.GetComponent<BoxCollider>().size;
            boxColliderCenter = _prefab.GetComponent<BoxCollider>().center;

            objectScale = _activeGameObject.transform.parent.GetComponent<ActiveObjectDependancies>().Prefab.transform.localScale;

            //Set the ActiveBox properties based on the scale, position and rotation of the selected object
            _boxSize = new Vector3(objectScale.x + boxMargin, objectScale.y + boxMargin, objectScale.z + boxMargin);
            _boxRotation = _activeGameObject.transform.eulerAngles;
            _boxPosition = _activeGameObject.transform.localPosition;

            //transform.localPosition = new Vector3(0, 0, 0);
            transform.localPosition = new Vector3(boxColliderCenter.x, boxColliderCenter.y, boxColliderCenter.z);
            //transform.localPosition = new Vector3(boxColliderCenter.x, boxColliderCenter.y, boxColliderCenter.z);
            //transform.position = boxColliderCenter;

            colliderXScale  = Vector3.Scale(_boxSize, boxColliderSize);
            transform.localScale = colliderXScale;
            transform.eulerAngles = _boxRotation;
        }
        catch
        {
            Debug.Log("Error from boxResizer script for Active GameObject Assignement");
        }

    }
}

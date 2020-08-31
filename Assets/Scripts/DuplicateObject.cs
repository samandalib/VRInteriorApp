using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateObject : MonoBehaviour
{

    public GameObject activeObjectManager;
    //public GameObject activeStatusShowGameObject;
    private GameObject _activeGameObject;
    private GameObject _parentObject;
    private Transform _grandParent;
    private Vector3 _parentPosition;
    private Quaternion _parentRotation;

    [SerializeField]
    private Color _baseColor;
    [SerializeField]
    private Color _ObjectColor;

    private GameObject _duplicated;


    // Update is called once per frame
    void Update()
    {
        _activeGameObject = activeObjectManager.GetComponent<ActiveObjectManager>().activeGameObject;
        try
        {
            //_baseColor = _activeGameObject.transform.parent.GetComponent<ActiveObjectDependancies>().Prefab.GetComponent<MeshRenderer>().material.color;
            //_baseColor = activeObjectManager.GetComponent<ActiveObjectManager>().objectBaseColor;
            _baseColor = ActiveObjectDependancies._baseColor;
        }
        catch
        {
            Debug.Log("There is no MeshRenderer available for the prefab");
        }
        _parentObject = _activeGameObject.transform.parent.gameObject;
        _grandParent = _parentObject.transform.parent;
        _parentPosition = _parentObject.transform.position;
        _parentRotation = _parentObject.transform.rotation;
    }

    public void Duplicate()
    {

        _duplicated = Instantiate(_parentObject, _parentPosition, _parentRotation);
        _duplicated.transform.SetParent(_grandParent);
        activeObjectManager.GetComponent<ActiveObjectManager>().activeGameObject = _duplicated.transform.GetChild(0).gameObject;
        //_ObjectColor = _activeGameObject.transform.parent.GetComponent<ActiveObjectDependancies>().Prefab.GetComponent<MeshRenderer>().material.color;
        _activeGameObject.transform.parent.GetComponent<ActiveObjectDependancies>().Prefab.GetComponent<MeshRenderer>().material.color = _baseColor;
        //_parentObject.transform.tag = "Untagged";
        //int _childCount = _duplicated.transform.GetChild(0).childCount;
        //Transform _dubpicatedTargetObject = _duplicated.transform.GetChild(0);

        //Destroy(_dubpicatedTargetObject.GetChild(_childCount - 1).gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObject : MonoBehaviour
{
    public GameObject activeObjectManager;
    private GameObject _parentObject;

    void Update()
    {
        _parentObject = activeObjectManager.GetComponent<ActiveObjectManager>().activeGameObject.transform.parent.gameObject;
    }

    public void DestroyObject()
    {
        Destroy(_parentObject);
    }
}

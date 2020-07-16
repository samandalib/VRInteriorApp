using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectDependancies : MonoBehaviour
{
    public GameObject UICanvas;
    public GameObject ActiveObjectManager;
    public GameObject Prefab;

    GameObject PrefabCopy;
    //Transform Target = GameObject.Find("TargetObject").transform;

    // Start is called before the first frame update
    private void Start()
    {
        //Prefab.AddComponent<BoxCollider>();
        //PrefabCopy = Instantiate(Prefab);
        //PrefabCopy.transform.SetParent(transform);
        //PrefabCopy.transform.SetAsLastSibling();

    }
    
    private void Update()
    {
        
    }

    public void ActivateCanvas()
    {
        UICanvas.SetActive(true);
    }

    public void DisActiveCanvas()
    {
        UICanvas.SetActive(false);
    }

    public void ActiveObjectManagerToLayerZero()
    {
        ActiveObjectManager.layer = 0;
    }
}

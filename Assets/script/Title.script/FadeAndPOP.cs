using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndPOP : MonoBehaviour
{

    public GameObject FADEObject;
    public GameObject POPObject;
    public GameObject DELETEObjext;

    bool once=false;

    private void Start()
    {
        POPObject.SetActive(false);
        once = false;
    }

    public void FadeAndPop()
    {
        if (!once)
        {
            
            FADEObject.gameObject.SetActive(false);
            POPObject.gameObject.SetActive(true);
            Destroy(DELETEObjext);
            once = true;
        }
    }
}

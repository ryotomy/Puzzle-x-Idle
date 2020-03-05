using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManualManager : MonoBehaviour
{
    public GameObject FadeTitleObject;
    public GameObject POPManualObject;
    public List<GameObject> ManualList = new List<GameObject>();
    public int manualpage=0;
    public int pagemin=0;
    public int pagemax;

    public GameObject Button;

    private void Start()
    {
        manualpage = 0;
        pagemax = ManualList.Count-1;
    }
    public void ManualButton()
    {
        Button.gameObject.GetComponent<Image>().color = Color.white;
        FadeTitleObject.SetActive(false);
        POPManualObject.SetActive(true);
    }

    public void ManualPreviousButton()
    {
        if (manualpage == pagemin)
        {
            FadeTitleObject.SetActive(true);
            POPManualObject.SetActive(false);
            return;
        }
        ManualList[manualpage].gameObject.SetActive(false);
        ManualList[manualpage-1].gameObject.SetActive(true);
        manualpage--;
        
    }
    public void ManualNextButton()
    {
        if (manualpage == pagemax)
        {
            GameManager.instance.LoadScene("mainstage");
            return;
        }
        ManualList[manualpage].gameObject.SetActive(false);
        ManualList[manualpage + 1].gameObject.SetActive(true);
        manualpage++;
    }
}

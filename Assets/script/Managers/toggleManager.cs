using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleManager : MonoBehaviour
{
    public List<Toggle> Toggle = new List<Toggle>();



    void Update()
    {
        if(BoardManager.instance.tileMOVE)
        for (int i = 0; i < Toggle.Count; i++)
        {
            Toggle[i].enabled = false;
        }
        else
        {
            for (int i = 0; i < Toggle.Count; i++)
            {
                BoardManager.instance.tileMOVE = false;
                Toggle[i].enabled = true;
                
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RATEUPUI : MonoBehaviour
{
    public static RATEUPUI instance;

    public Text SPATKText;
    public Text SPATKSPDLText;
    public Text SPSPDText;
    public Text SPCRIText;
    public Text SPEXATKText;

    public int spatk=2;
    public int spatkspd=1;
    public int spspd=1;
    public int spcri=1;
    public int spexatk=1;

    public void ATKRATEButtom()
    {
        if (spatk <= LVStatusUI.instance.SP)
        {
            StatusUI.instance.ATKRATEUP();
            LVStatusUI.instance.SP -= spatk;
        }
    }
    public void ATKSPDRATEButtom()
    {
        if (spatkspd <= LVStatusUI.instance.SP)
        {
            StatusUI.instance.ATKSPDRATEUP();
            LVStatusUI.instance.SP -= spatkspd;
        }
    }
    public void SPDRATEButtom()
    {
        if (spspd <= LVStatusUI.instance.SP)
        {
            StatusUI.instance.SPDRATEUP();
            LVStatusUI.instance.SP -= spspd;
        }
    }
    public void CRIRATEButtom()
    {
        if (spcri <= LVStatusUI.instance.SP)
        {
            StatusUI.instance.CRIRATEUP();
            LVStatusUI.instance.SP -= spcri;
        }
    }
    public void EXATKRATEButtom()
    {
        if (spexatk <= LVStatusUI.instance.SP)
        {
            StatusUI.instance.EXATKRATEUP();
            LVStatusUI.instance.SP -= spexatk;
        }
    }

    private void Awake()
    {
        instance = GetComponent<RATEUPUI>();
        SPATKText.text=spatk.ToString();
        SPATKSPDLText.text=spatkspd.ToString();
        SPSPDText.text=spspd.ToString();
        SPCRIText.text=spcri.ToString();
        SPEXATKText.text=spexatk.ToString();
}

}

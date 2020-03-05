using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
	public static StatusUI instance;
	// 攻撃力を表示するGUIText
	public Text ATKUIText;
	public Text ATKText;
	public List<Text> ATKRATEText = new List<Text>();
	public Text ATKSPDText;
    public List<Text> ATKSPDRATEText = new List<Text>();
    public Text SPDText;
    public List<Text> SPDRATEText = new List<Text>();
    public Text CRIText;
    public List<Text> CRIRATEText = new List<Text>();
    public Text EXATKText;
    public List<Text> EXATKRATEText = new List<Text>();

  
    // 攻撃力
    public int atk;
	public int atkspd;
	public int spd;
	public double cri;

	public int atkrate;
	public int atkspdrate;
	public int spdrate;
	public double crirate;

    public int exatk;
    public int exatkrate;

    public Text ATKRATEAFText;
    public Text ATKSPDRATEAFText;
    public Text SPDRATEAFText;
    public Text CRIRATEAFText;
    public Text EXATKRATEAFText;

    public int atkrateaf;
    public int spdrateaf;
    public int atkspdrateaf;
    public double crirateaf;
    public int exatkrateaf;


    public int ATKRATE
	{
		get
		{
			return atkrate;
		}

		set
		{
			atkrate = value ;
            for (int i = 0; i < ATKRATEText.Count; i++)
                ATKRATEText[i].text = atkrate.ToString();
            ATKRATEAFText.text= (atkrate+atkrateaf).ToString();

        }
	}
	public int ATKSPDRATE
	{
		get
		{
			return atkspdrate;
		}

		set
		{
			atkspdrate = value;
            for (int i = 0; i < ATKRATEText.Count; i++)
                ATKSPDRATEText[i].text = atkspdrate.ToString();
            ATKSPDRATEAFText.text = (atkspdrate + atkspdrateaf).ToString();
        }
	}

	public int SPDRATE
	{
		get
		{
			return spdrate;
		}

		set
		{
			spdrate = value;
            for (int i = 0; i < SPDRATEText.Count; i++)
                SPDRATEText[i].text = spdrate.ToString();
            SPDRATEAFText.text = (spdrate + spdrateaf).ToString();
        }
	}

	public double CRIRATE
	{
		get
		{
			return crirate;
		}

		set
		{
			crirate = value;
            for (int i = 0; i <CRIRATEText.Count; i++)
                CRIRATEText[i].text = crirate.ToString();
            CRIRATEAFText.text = (crirate + crirateaf).ToString();
        }
	}
    public int EXATKRATE
    {
        get
        {
            return exatkrate;
        }

        set
        {
            exatkrate = value;
            for (int i = 0; i < EXATKRATEText.Count; i++)
                EXATKRATEText[i].text = exatkrate.ToString();
            EXATKRATEAFText.text = (exatkrate + exatkrateaf).ToString();
        }
    }

    public float ComboRate = 1;
    public int ATK
    {
		get
		{
			return atk;
		}

		set
		{
			atk =value*atkrate+1;
			ATKUIText.text = atk.ToString();
			ATKText.text = atk.ToString();

		}       
    }
	public int ATKSPD
	{
		get
		{
			return atkspd;
		}

		set
		{
			atkspd =(int)((value*atkspdrate+100)*ComboRate);
			moveplayer.instance.ATKSPD = atkspd;
			ATKSPDText.text = atkspd.ToString();

		}
	}

	public int SPD
	{
		get
		{
			return spd;
		}

		set
		{
			spd = (int)((value * spdrate + 100)*ComboRate);
			moveplayer.instance.SPD = spd;
			SPDText.text = spd.ToString();

		}
	}
	public double CRI
	{
		get
		{
			return cri;
		}

		set
		{
			cri =value*crirate+5;
			CRIText.text = cri.ToString();

		}
	}
    public int EXATK
    {
        get
        {
            return exatk;
        }

        set
        {
            exatk = value;
            EXATKText.text = exatk.ToString();

        }
    }

    public void ATKRATEUP()
    {
        ATKRATE += atkrateaf;
        GUIManager.instance.ATK = GUIManager.instance.score1;
    }
    public void ATKSPDRATEUP()
    {
        ATKSPDRATE += atkspdrateaf;
        GUIManager.instance.ATKSPD = GUIManager.instance.score2;
    }
    public void SPDRATEUP()
    {
        SPDRATE += spdrateaf;
        GUIManager.instance.SPD = GUIManager.instance.score3;
    }
    public void CRIRATEUP()
    {
        CRIRATE += crirateaf;
        GUIManager.instance.CRI = GUIManager.instance.score4;
    }
    public void EXATKRATEUP()
    {
        EXATKRATE += exatkrateaf;
        GUIManager.instance.ATKTIMES = GUIManager.instance.score5;
    }

    public float ATKSPDCOMBOUP
    {
       
        set
        {
            atkspd = (int)(value * atkspd);
            moveplayer.instance.ATKSPD = atkspd;
            ATKSPDText.text = atkspd.ToString();

        }
    }

    public float SPDCOMBOUP
    {
       
        set
        {
            spd = (int)(value * spd);
            moveplayer.instance.SPD = spd;
            SPDText.text = spd.ToString();

        }
    }

    void Awake()
	{

        atkrateaf = 1;
        spdrateaf = 5;
        atkspdrateaf = 5;
        crirateaf = 0.5;
        exatkrateaf = 1;
        instance = GetComponent<StatusUI>();
		ATKRATE = 1;
		ATKSPDRATE = 5;
		SPDRATE = 5;
		CRIRATE = 0.5;
        EXATKRATE = 1;


        /**参考
         atkrateaf = 1;
        spdrateaf = 1;
        atkspdrateaf = 1;
        crirateaf = 0.2;
        exatkrateaf = 1;
        instance = GetComponent<StatusUI>();
		ATKRATE = 1;
		ATKSPDRATE = 1;
		SPDRATE = 1;
		CRIRATE = 0.2;
        EXATKRATE = 1;
        **/
    }


}

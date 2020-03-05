using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PurchaseUI : MonoBehaviour
{

   
    public List<Text> UI = new List<Text>();
    public List<Text> MovecountrateText = new List<Text>();
    public static PurchaseUI instance;
    public int movemoney=5;
    public int movecountrate=1;

    // Start is called before the first frame update
    public void OnClick()
    {

        Debug.Log("押された!");
        if (money.instance.Money >= movemoney)
        {

            GUIManager.instance.MoveCounter+=Movecountrate;
            money.instance.Money -= movemoney;
            Movemoney += 5;

        }
    }
    public int Movemoney
    {
        get
        {
            return movemoney;
        }
        set
        {
            movemoney = value;
            for (int i = 0; i < UI.Count; i++)
            {
                if(movemoney<10)
                    UI[i].text = "-"+Movemoney.ToString();
                else
                    UI[i].text = "-"+ Movemoney.ToString();
            }
        }
    }
    public int Movecountrate
    {
        get
        {
            return movecountrate;
        }

        set
        {

            movecountrate = value;
            for (int i = 0; i < MovecountrateText.Count; i++)
                MovecountrateText[i].text = movecountrate.ToString();

        }
    }
    private void Awake()
    {
        Movemoney = 5;
        Movecountrate = 1;
        instance = GetComponent<PurchaseUI>();
            

    }
}

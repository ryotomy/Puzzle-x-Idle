using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ComboBonus : MonoBehaviour
{
    public static ComboBonus instance;
    public Text BonusTimeText;
    public Text BonusTimeLabelText;
    public Text BonusComboText;

    public float BonusTime;
    public float BonusPlusTime;

    [SerializeField]
    private GameObject BonuscomboPoP=default;

    private void Awake()
    {
        instance = GetComponent<ComboBonus>();
        BonusTime = 0;
        BonusTimeText.gameObject.SetActive(false);
        BonusTimeLabelText.gameObject.SetActive(false);
        BonusComboText.gameObject.SetActive(false);
        //BonusTimeText.color = new Color(0, 0, 0, 0);
        BonusTimeLabelText.color = new Color(0, 0, 0, 0);
    }

    public float MaincomboRate;

    void Update()
    {
        if (StartTime.instance.pause)
        {
            return;
        }
        if (BonusTime > 0)
        {
            StatusUI.instance.ComboRate = MaincomboRate / 100;
            GUIManager.instance.ATKSPD = GUIManager.instance.score2;
            GUIManager.instance.SPD = GUIManager.instance.score3;



            BonusTime -= Time.deltaTime;
            if (BonusPlusTime > 0)
            {
                BonusPlusTime -= Time.deltaTime;
                BonusTimeText.text = "ComboBonus:" + BonusTime.ToString("f1") + "<color=#0000FF> + " + plusTimetemp.ToString() + "</color>";

                
                BonusComboText.text = combo + " Combo Bonus!";
            }
            else
            {
                BonusTimeText.text = "ComboBonus:" + BonusTime.ToString("f1");
                BonusComboText.gameObject.SetActive(false);
            }
            BonusTimeLabelText.text = "ATKSPD&WALKSPD "+MaincomboRate.ToString("f0")+"%UP";

           

        }
        if (BonusTime <= 0)
        {
            StatusUI.instance.ComboRate = 100/100;
            GUIManager.instance.ATKSPD = GUIManager.instance.score2;
            GUIManager.instance.SPD = GUIManager.instance.score3;

            BonusTime = 0;
            BonusTimeText.text = BonusTime.ToString("f1");
            //BonusTimeText.color = new Color(0, 0, 0, 0);
            BonusTimeLabelText.color = new Color(0, 0, 0, 0);
            BonusComboText.gameObject.SetActive(false);
            BonusTimeText.gameObject.SetActive(false);
            BonusTimeLabelText.gameObject.SetActive(false);

        }
       

    }
    float plusTimetemp;
    public string combo;
    public void BonusTimePlus(float plusTime,int combonumber,float comborate)
    {
        if(comborate>MaincomboRate)
        MaincomboRate = comborate;

        if (BonusTime == 0)
        {
            BonusTimeText.gameObject.GetComponent<Blink2>().setTimezero();
            BonusTimeLabelText.gameObject.GetComponent<Blink2>().setTimezero();
            
        }
        BonusTime +=plusTime;

        if (BonusPlusTime > 2.5f)
        {
            plusTimetemp += plusTime;
            combo+=","+ combonumber.ToString();
        }
        else
        {
            plusTimetemp = plusTime;
            combo = combonumber.ToString();
            BonusComboText.gameObject.SetActive(true);

            GameObject obj = Instantiate(BonuscomboPoP);
            obj.transform.SetParent(transform, false);
        }

        BonusTimeText.text = "ComboBonus:" + BonusTime.ToString("f1") + "<color=#0000FF> + " + plusTimetemp.ToString()+ "</color>";
        BonusTimeLabelText.text = "ATKSPD&WALKSPD "+comborate.ToString("f0")+"%UP";
        
        if (BonusTime > 0)
        {
             


            BonusPlusTime = 3f;
        }

            BonusTimeText.gameObject.SetActive(true);
            BonusTimeLabelText.gameObject.SetActive(true);
        
    }
   
}

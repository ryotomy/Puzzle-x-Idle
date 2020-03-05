using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class money : MonoBehaviour
{
	public static money instance;
    // スコアを表示するGUIText
    public Text scoreText;
    public Text scoresubText;
    // スコア
    public int mainmoney;

	public int Money
	{
		get
		{
			return mainmoney;
		}
		set
		{
			mainmoney = value;
			scoreText.text = mainmoney.ToString();
            scoresubText.text = mainmoney.ToString();

        }
	}

    public void addmoney(int money)
    {
        mainmoney = mainmoney + money;
    }
	void Awake()
	{
		instance = GetComponent<money>();
		Money = 0;
	}

    /**
    private void Update()
    {
        if (mainmoney == 100)
        {
            GUIManager.instance.GameOver();
            StartTime.instance.pause = true;
            if (moveplayer.instance.state == moveplayer.playerState.walk)
            {

                moveplayer.instance._anim.speed = 0;
                return;
            }
        }
    }
    **/
}

// Copyright (c) 2017 Razeware LLC


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour
{
	public static GUIManager instance;

	public GameObject gameOverPanel;
	public Text yourScoreTxt;
	public Text highScoreTxt;

	public Text score1Txt;
	public Text ATKTxt;
	public Text score2Txt;
	public Text ASTxt;
	public Text score3Txt;
	public Text SPDTxt;
	public Text score4Txt;
	public Text CRTTxt;
    public Text score5Txt;
    public Text ATKTIMESTxt;

    public Text moveCounterTxt;
	public Text MOVETxt;
	public Text MOVE2Txt;
	public int score1;
	public int score2;
	public int score3;
	public int score4;
    public int score5;
	[SerializeField]
	private int moveCounter;

	public int ATK
	{
		get
		{
			return score1;
		}

		set
		{
			score1 = value;

			script.ATK = score1;
			score1Txt.text = score1.ToString();
			ATKTxt.text = score1.ToString();
		}
	}

	public int ATKSPD
	{
		get
		{
			return score2;
		}

		set
		{
			score2 = value;

			script.ATKSPD = score2;
			score2Txt.text = score2.ToString();
			ASTxt.text = score2.ToString();
		}
	}

	public int SPD
	{
		get
		{
			return score3;
		}

		set
		{
			score3 = value;
			script.SPD = score3;
			score3Txt.text = score3.ToString();
			SPDTxt.text = score3.ToString();
		}
	}
   

    public int CRI
	{
		get
		{
			return score4;
		}

		set
		{
			score4 = value;
			script.CRI = value;
			score4Txt.text = score4.ToString();
			CRTTxt.text = score4.ToString();
		}
	}

    public int ATKTIMES
    {
        get
        {
            return score5;
        }

        set
        {
            score5 += value*StatusUI.instance.EXATKRATE;
            script.EXATK = score5;
            score5Txt.text = score5.ToString();
            ATKTIMESTxt.text = score5.ToString();
        }
    }

    public void ATKTIMESminus()
    {

        score5--;
            script.EXATK = score5;
            score5Txt.text = score5.ToString();
            ATKTIMESTxt.text = score5.ToString();
     
    }

    public int MoveCounter
	{
		get
		{
			return moveCounter;
		}

		set
		{
			moveCounter = value;

			moveCounterTxt.text = moveCounter.ToString();
			MOVETxt.text = moveCounter.ToString();
			MOVE2Txt.text = moveCounter.ToString();
		}
	}

	GameObject statusui;
	StatusUI script;

	void Awake()
	{
		statusui = GameObject.Find("StatusUI");
		script = statusui.GetComponent<StatusUI>();

		ATK = 0; ATKSPD = 0; SPD = 0; CRI = 0;
		instance = GetComponent<GUIManager>();
		moveCounter = 1;
		moveCounterTxt.text = moveCounter.ToString();
		MOVETxt.text = moveCounter.ToString();
		MOVE2Txt.text = moveCounter.ToString();

	}

	public void GameOver() {
    
		gameOverPanel.SetActive(true);

		
	}

}

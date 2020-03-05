using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LVStatusUI : MonoBehaviour
{
    public static LVStatusUI instance;

    public Text requireEXPText;
    public Text EXPText;
    public Text LEVELText;
    public List<Text> SPText = new List<Text>();

    public GameObject LevelUPPOP;

    int level;

    public int exp=0;
    public int requireexp;
    public int requirebase=10;
    public int sp = 0;

    private void Awake()
    {
        instance = GetComponent<LVStatusUI>();
    }

    private void Start()
    {
        level = 1;
        requireexp = requirebase*level;
        EXP = 0;
        SP = 0;
    }

    public int SP
    {
        get
        {
            return sp;
        }
        set
        {
            sp = value;
            for (int i = 0; i < SPText.Count; i++)
                SPText[i].text = sp.ToString();
        }
    }
   
    public int EXP
    {
        get
        {
            return exp;
        }
        set
        {
            exp = value;

            if (exp >= requireexp)
            {
                exp = exp % requireexp;

                level++;SP++;

                requireexp= requirebase * level;
                
                GameObject obj = Instantiate(LevelUPPOP);
                obj.transform.SetParent(GameObject.Find("main window").transform, false);
            }
            requireEXPText.text = requireexp.ToString();
            EXPText.text = exp.ToString();
            LEVELText.text = level.ToString();
        }
    }
}

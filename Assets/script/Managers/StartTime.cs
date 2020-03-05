using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartTime : MonoBehaviour
{
    public Text CountTimeText;
    public GameObject StartPanel;
    public GameObject Start;
    
    public static StartTime instance;
    public float starttime = 4f;
    // Start is called before the first frame update

    public bool pause = true;
    
    private void Awake()
    {
        pause = true;
        StartPanel.gameObject.SetActive(true);
        instance = GetComponent<StartTime>();
        CountTimeText.text="3";
    }
    private bool Onse=false;
    void Update()
    {
        starttime -= Time.deltaTime;
        if ( 2f < starttime && starttime <= 3f)
        {
            CountTimeText.text = "2";
        }
        if (1f < starttime && starttime <= 2f)
        {
            CountTimeText.text = "1";
        }
        if (0f < starttime && starttime <= 1f)
        {
            if (!Onse) {
                GameObject obj = Instantiate(Start);
                obj.transform.SetParent(transform, false);
                Destroy(StartPanel.gameObject);

                moveplayer.instance._anim.speed = moveplayer.instance.animspeed;
                pause = false;
                Onse = true;
            }

        }
        if (starttime <= 0)
        {
         
        }
    }
}

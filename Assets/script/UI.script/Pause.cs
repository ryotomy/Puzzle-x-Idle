using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public GameObject pauseobject;
    private bool active=false;

    private void Start()
    {
        active = false;
    }
    private void Update()
    {
        if (pauseobject.gameObject.activeSelf)
        {
            active = true;
            StartTime.instance.pause = true;
            moveplayer.instance._anim.speed =0;
        }
        else if (active)
        {
            active = false;
            StartTime.instance.pause = false;
            moveplayer.instance._anim.speed = moveplayer.instance.animspeed;
        }
    }
}

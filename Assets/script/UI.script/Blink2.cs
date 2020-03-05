using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink2 : MonoBehaviour
{
    public static Blink2 instance;
    //public
    public float speed = 1.0f;

    //private
    private Text text;
    private Image image;
    public float time;
    public bool ontime=false;

    private void Awake()
    {
        instance = GetComponent<Blink2>();
        ontime = false;
    }

    private enum ObjType
    {
        TEXT,
        IMAGE
    };
    private ObjType thisObjType = ObjType.TEXT;

    void Start()
    {
        //アタッチしてるオブジェクトを判別
        if (this.gameObject.GetComponent<Image>())
        {
            thisObjType = ObjType.IMAGE;
            image = this.gameObject.GetComponent<Image>();
        }
        else if (this.gameObject.GetComponent<Text>())
        {
            thisObjType = ObjType.TEXT;
            text = this.gameObject.GetComponent<Text>();
        }
    }

    void Update()
    {
       
        //オブジェクトのAlpha値を更新
        if (thisObjType == ObjType.IMAGE)
        {
            image.color = GetAlphaColor(image.color);
        }
        else if (thisObjType == ObjType.TEXT)
        {
            text.color = GetAlphaColor(text.color);
        }

      
    }

    public void setTimezero()
    {
        time = 0;
        Debug.Log("1");
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 8.0f * speed;
        if(Mathf.Sin(time*0.1f)>=0)
        color.a = Mathf.Sin(time*0.1f);
        else
        {
            color.a = -Mathf.Sin(time*0.1f);
        }

        return color;
    }
}
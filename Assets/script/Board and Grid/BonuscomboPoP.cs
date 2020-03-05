using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;//Convertを使う場合は忘れずに書く

//ダメージ表記用ｃｓ

public class BonuscomboPoP : MonoBehaviour
{
    public Text comboText;
    //　フェードアウトするスピード	
    [SerializeField]
    private float fadeOutSpeed = 0.7f;
    //　移動値
    [SerializeField]
    private float moveSpeed = 0.8f;
    GameObject gameobject;

    void Start()
    {
        comboText = GetComponentInChildren<Text>();
      
        comboText.text = ComboBonus.instance.combo + " Combo Bonus!";
    }

    void LateUpdate()
    {
        if (StartTime.instance.pause)
        {
            return;
        }
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        comboText.color = Color.Lerp(comboText.color, new Color(0f, 0f, 0f, 0f), fadeOutSpeed * Time.deltaTime*2.5f);

        if (comboText.color.a <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
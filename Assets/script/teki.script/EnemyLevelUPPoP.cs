using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;//Convertを使う場合は忘れずに書く

//ダメージ表記用ｃｓ

public class EnemyLevelUPPoP : MonoBehaviour
{
    public Text Text;
    //　フェードアウトするスピード	
    [SerializeField]
    private float fadeOutSpeed = 0.7f;
    //　移動値
    [SerializeField]
    private float moveSpeed = 0.8f;
    GameObject gameobject;

    void Start()
    {
        Text = GetComponentInChildren<Text>();

        Text.text = "Enemy Level UP!";
    }

    void LateUpdate()
    {
        if (StartTime.instance.pause)
        {
            return;
        }
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        Text.color = Color.Lerp(Text.color, new Color(0f, 0f, 0f, 0f), fadeOutSpeed * Time.deltaTime*3.5f);

        if (Text.color.a <= 0.001f)
        {
            Destroy(gameObject);
        }
    }
}
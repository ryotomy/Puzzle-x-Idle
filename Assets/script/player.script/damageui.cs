using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;//Convertを使う場合は忘れずに書く
 
//ダメージ表記用ｃｓ

public class damageui : MonoBehaviour
{
	public Text damageText;
	//　フェードアウトするスピード	

	//　移動値
	[SerializeField]
	private float moveSpeed = 0.8f;
	GameObject gameobject;
    private static Color color=new Color(0f, 0f, 0f, 0f);
    float a_color = 1;
    float rate=1.5f;
	void Start()
	{
		damageText = GetComponentInChildren<Text>();
		moveplayer moveplayer;
		teki teki;
		gameobject = GameObject.Find("y1");
		moveplayer = gameobject.GetComponent<moveplayer>();
		name = moveplayer.Name;
		gameobject = GameObject.Find(name);
		teki = gameobject.GetComponent<teki>();
		int damage=teki.ATK;
		damageText.text = damage.ToString();
        if (teki.onCRI)
        {
            color = new Color(0f, 0f, 0f, a_color);
            damageText.fontSize = 200;
            moveSpeed = 1.5f;
        }
        if (moveplayer.instance.Onatktimes)
        {
          
            rate = 1.5f;
        }
    }
    private void Update()
    {
        if (StartTime.instance.pause)
        {
            return;
        }
        a_color -= Time.deltaTime*rate;
    }
    void LateUpdate()
	{
        if (StartTime.instance.pause)
        {
            return;
        }
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

		damageText.color = new Color(0f, 0f, 0f, a_color);



        if (damageText.color.a <= 0.1f)
		{
			Destroy(gameObject);
		}
	}
}
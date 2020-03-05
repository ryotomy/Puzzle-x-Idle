using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
public class teki : MonoBehaviour
{

	//　敵のMaxHP
	[SerializeField]
	
	//　敵のHP
	
	public int hp;
	//　HP表示用UI
	[SerializeField]
	private GameObject HPUI=default;
	//　HP表示用スライダー
	private Slider hpSlider;
	//　次に敵が出現するまでの時間
	[SerializeField] float appearNextTime;
	//　待ち時間計測フィールド
	private float elapsedTime;
	[SerializeField]
	public int enemymoney;

    public double moneyrate;
    int basemoneylow=2;
    int basemoneyhigh=4;

    public int maxHp;
    public double hprate;
    int basehplow = 3;
    int basehphigh = 4;

    //経験値
    public int exp;
    int exprate;
    int baseexp=1;

	GameObject gameobject;
	GameObject damageui;

	//ダメージ受けた時の点滅用変数
	private SpriteRenderer Renderer;
	public bool on_damage = false;
    //クリティカル計算用変数
	public int ATK;
    public double CRI;
    public double random;
    public bool onCRI;

    void Awake()
	{
        moneyrate = 1+(tekipop.instance.ENEMYCOUNT/5)*1.5;
        hprate = 1 + (tekipop.instance.ENEMYCOUNT / 5) * 1;
        exprate = 1+(tekipop.instance.ENEMYCOUNT / 5) * 1;

        //敵HP設定
        maxHp = Random.Range((int)(basehplow * hprate), (int)(basehphigh * hprate));
        hp = maxHp;

        hpSlider = HPUI.transform.Find("HPBar").GetComponent<Slider>();
		hpSlider.value = 1f;
        //落とすお金設定
        enemymoney = Random.Range((int)(basemoneylow*moneyrate), (int)(basemoneyhigh*moneyrate));
        //経験値設定
        exp = baseexp * exprate;

        Renderer = gameObject.GetComponent<SpriteRenderer>();
	}

   
	void Update()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (on_damage)
		{
	
			Renderer.color = new Color(1f, 1f, 1f, 0);
		}


	}
	public void damage()
	{
        onCRI = false;
		on_damage = true;
		StartCoroutine("WaitForIt");

		ATK = StatusUI.instance.ATK;
        //クリティカル判定
        CRI = StatusUI.instance.CRI;
        random = Random.value;
        if (random <= CRI / 100)
        {
            ATK *= 3;
            onCRI = true;
        }

    	Vector3 tmp = this.transform.position;
		GameObject obj = Instantiate(Resources.Load("damageui"), new Vector3(tmp.x + 0.5f, tmp.y - 2.2f, tmp.z), Quaternion.identity) as GameObject;
        //ダメージ表記生成

        //HP減算・破壊判定

        hp = hp - ATK;

		SetHp(hp);
		if (hp <= 0)
		{

            moveplayer.instance.SetState(moveplayer.playerState.finishwait);
			money.instance.Money += enemymoney;
            LVStatusUI.instance.EXP += exp;
			gameobject = GameObject.Find("tekipop");
			gameobject.GetComponent<tekipop>().destroyenemy();
			Destroy(this.gameObject);  //ゲームオブジェクトが破壊される

		}

	}
	public void SetHp(int hp)
	{
		this.hp = hp;

		//　HP表示用UIのアップデート
		UpdateHPValue();

		if (hp <= 0)
		{
			//　HP表示用UIを非表示にする
			HideStatusUI();

		}
	}

	public int GetHp()
	{
		return hp;
	}

	public int GetMaxHp()
	{
		return maxHp;
	}

	//　死んだらHPUIを非表示にする
	public void HideStatusUI()
	{
		HPUI.SetActive(false);
	}

	public void UpdateHPValue()
	{
		hpSlider.value = (float)GetHp() / (float)GetMaxHp();
	}


	IEnumerator WaitForIt()
	{
		// 0.2秒間処理を止める
		yield return new WaitForSeconds(0.05f);

		// １秒後ダメージフラグをfalseにして点滅を戻す
		on_damage = false;
		Renderer.color = new Color(1f, 1f, 1f, 1f);
	}
}


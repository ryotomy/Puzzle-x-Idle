using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class moveplayer : MonoBehaviour
{
	public static moveplayer instance;
    public enum playerState
    {
		wait,
        attack,
        atkplus,
        walk,
		finishwait
    };
    private CharacterController playerController;
    public Animator _anim;

    [SerializeField]
    private float walkSpeed = 0.5f;
       //　速度
    private Vector3 velocity;
    [SerializeField]
    public playerState state;
    [SerializeField]
    public float timeOut = 1.0f;
    [SerializeField]
    private float timeElapsed;
	[SerializeField]
	private float timeWait;
	[SerializeField] GameObject effect=default;
	bool isCalledOnce = false;
	[SerializeField]
	public float animspeed=1.0f;
    private float Animspeed = 1.0f;

    private int hp1;
    teki teki;
GameObject gameobject;

	public string Name;

	public float atkspd;



   
    public float ATKSPD
	{
		get
		{
			return atkspd;
		}
		set
		{
			atkspd = timeOut / (value / 100);
            animspeed=Animspeed / ( 100/ value);

        }

	}

	public float spd;

	public float SPD
	{
		get
		{
			return spd;
		}
		set
		{
			spd = walkSpeed * (value / 100);
		}
	}

	private void Awake()
	{
	instance= GetComponent<moveplayer>();
		atkspd = timeOut;
		spd = walkSpeed;

        animspeed = 1.0f;
        _anim.speed = 0;
    }

	void Start()
    {
        _anim = GetComponent<Animator>();
        SetState(playerState.walk);

    }
    bool FirstAttack;
    public bool Onatktimes;
    bool staycollider;
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
		Name = other.gameObject.name;
        FirstAttack = true;
		SetState(playerState.wait);

    }
    void OnTriggerExit2D(Collider2D other)
    {
       
        _anim.speed = animspeed;
        SetState(playerState.finishwait);
        staycollider = false;

        EnemycountUI.instance.EnemyCount();
    }
    void OnTriggerStay2D(Collider2D other)
    {

        EnemyInformation.instance.GetInformation(other.gameObject.GetComponent<SpriteRenderer>().sprite, 
            other.gameObject.GetComponent<teki>().maxHp, other.gameObject.GetComponent<teki>().enemymoney, other.gameObject.GetComponent<teki>().exp);
        staycollider = true;

    }
    void Update()
    {
        if (StartTime.instance.pause)
        {
            return;
        }
		if (state == playerState.wait)
		{
			_anim.speed = 0;
			timeElapsed += Time.deltaTime;

			if (timeElapsed >= 0.01f)
			{
				_anim.speed = animspeed;
                timeElapsed = 0.0f;
                if (GUIManager.instance.ATKTIMES > 0)
                {
                 
                    
                    _anim.speed = animspeed * 10;
                    SetState(playerState.atkplus);

                }
                else
                     SetState(playerState.attack);
				


			}
		}
        if (state == playerState.atkplus)
        {
            if (GUIManager.instance.ATKTIMES == 0)
            {
                SetState(playerState.attack);
            }
            if (FirstAttack)
            {
                attack();
            }
            FirstAttack = false;
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= atkspd/10)
            {
                if (staycollider)
                {
                    Onatktimes = true;
                    timeElapsed = 0.0f;
                    attack();
                    GUIManager.instance.ATKTIMESminus();
                  
                    

                }
                //基本値：攻撃モーション・エフェクトのスピード倍率＝１＝timeoutの基本倍率）
            }
        }
            if (state == playerState.attack)
        {
            if (FirstAttack)
            {
               attack();
            }
                FirstAttack = false;

            timeElapsed += Time.deltaTime;

            if (timeElapsed >= atkspd)
            {
                FirstAttack = true;
                if (GUIManager.instance.ATKTIMES > 0)
                {
                   
                    _anim.speed = animspeed * 10;
                    timeElapsed = 0.0f;
                    SetState(playerState.atkplus);
                }
                
                timeElapsed = 0.0f;
               
                //基本値：攻撃モーション・エフェクトのスピード倍率＝１＝timeoutの基本倍率）
            }
        }
        if (state == playerState.walk)
        {
            if (!isCalledOnce)
            {
                walk();
                isCalledOnce = true;

            }
            this.transform.position += new Vector3(spd, 0, 0);
			//ここで移動速度を変化できる
        }
		if (state == playerState.finishwait)
		{
			timeElapsed += Time.deltaTime;
			if (timeElapsed == 0.0f)
			{
				Vector3 tmp = GameObject.Find(Name).transform.position;
				GameObject.Instantiate(effect, new Vector3(tmp.x, tmp.y - 0.1f, tmp.z), Quaternion.identity);
				_anim.Play("attack", 0, 0.0f);
			}
			if (timeElapsed >= atkspd)
			{

				SetState(playerState.walk);
				timeElapsed = 0.0f;


			}
		}
	}


    public void SetState(playerState tempState)
    {
		if (tempState == playerState.wait)
		{
			state = tempState;

		}
        if (tempState == playerState.atkplus)
        {
            state = tempState;

        }
        else if (tempState == playerState.attack)
        {
            Onatktimes = false;
            _anim.speed = animspeed;
            state =tempState;
           
        }
        else if (tempState == playerState.walk)
        {
            state = tempState;
			walk();
            EnemyInformation.instance.Blank();
        }
		else if (tempState == playerState.finishwait)
		{
			state = tempState;

		}
	}
    public void attack()
    {
        Vector3 tmp = GameObject.Find(Name).transform.position;
        GameObject.Instantiate(effect, new Vector3(tmp.x, tmp.y - 0.1f, tmp.z), Quaternion.identity);
        _anim.Play("attack", 0, 0.0f);
        gameobject = GameObject.Find(Name);
   gameobject.GetComponent<teki>().damage();

    }
    public void walk()
    {
        _anim.Play("walk", 0, 0.0f);
    }
}

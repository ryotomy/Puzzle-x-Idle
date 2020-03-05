using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class tekipop : MonoBehaviour
{

    //　出現させる敵を入れておく
    [SerializeField] public GameObject[] enemys;
    //　今何人の敵を出現させたか（総数）
    [SerializeField] private int numberOfEnemys;
    // 敵の出現判定
    [SerializeField] private int popenemy;
	public int space=3; // 生成する間隔
	public Vector3 createPos; // 生成する場所
	public int i;
    public static tekipop instance;

    private void Awake()
    {
        instance = GetComponent<tekipop>();
    }
    // Use this for initialization
    void Start()
    {
        numberOfEnemys = 0;
    }

    void Update()
    {
        //　この場所から出現する最大数を超えてたら何もしない
        if (popenemy >= 2)
        {
            return;
        }
  
            AppearEnemy();
            popenemy += 1;
        
    }


    //　敵出現メソッド
    void AppearEnemy()
    {
        //　出現させる敵をランダムに選ぶ
        var randomValue = Random.Range(0, enemys.Length);


		GameObject.Instantiate(enemys[randomValue],new Vector3(1.8f * i+this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);

		i++;
        numberOfEnemys++;
    }
    public void destroyenemy()
    {
        popenemy -= 1;
    }
    public int ENEMYCOUNT
    {
        get
        {
            return numberOfEnemys;
        }
        set
        {
            numberOfEnemys = value;
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public static TimeUI instance;

    public GameObject GameOverObject; 
    [SerializeField]
    public Text Timetext;

    public float countTime = 300;
    // Use this for initialization

    private void Awake()
    {
        instance = GetComponent<TimeUI>();
    }

    void Start()
    {
        countTime = 300;
        Timetext.text = countTime.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTime.instance.pause)
            return;
        countTime -= Time.deltaTime; //スタートしてからの秒数を格納
        Timetext.text = countTime.ToString("F2"); //小数2桁にして表示
        if (countTime <= 0)
        {
            countTime = 0;
            Timetext.text = countTime.ToString("F2");
            StartTime.instance.pause = true;
            moveplayer.instance._anim.speed = 0;
            GameOverObject.SetActive(true);
            
        }
    }
}
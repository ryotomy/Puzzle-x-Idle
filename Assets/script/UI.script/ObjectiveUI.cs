using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField]
    GameObject Slider=default;
    Slider _slider;

    public Text ObjestivePointText;
    public Text CurrentPointText;

    public int Objective=100;

    public Text RemainTimeText;

    void Start()
    {

        Objective = 100;

        // スライダーを取得する
        _slider = Slider.GetComponent<Slider>();

        _slider.maxValue = Objective;
        ObjestivePointText.text = Objective.ToString();

        _slider.value = 0;
        CurrentPointText.text = "0";
    }

   
    void Update()
    {
        if (StartTime.instance.pause)
            return;

       
        _slider.value =EnemycountUI.instance.defeatedEnemies;
        CurrentPointText.text = EnemycountUI.instance.defeatedEnemies.ToString();


        if (_slider.value == _slider.maxValue)
        {
            GUIManager.instance.GameOver();
            StartTime.instance.pause = true;
            RemainTimeText.text=TimeUI.instance.countTime.ToString("F2");

            if (moveplayer.instance.state == moveplayer.playerState.walk)
            {

                moveplayer.instance._anim.speed = 0;
                return;
            }
        }


        //金を目標にするときのコード
        /**
        _slider.maxValue = Objective;
        ObjestivePointText.text = Objective.ToString();
        _slider.value =money.instance.mainmoney;
        CurrentPointText.text = money.instance.mainmoney.ToString();
        **/


    }
}
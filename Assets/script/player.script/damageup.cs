using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageup : MonoBehaviour
{
    int s;
    GameObject gameobject;
    // Start is called before the first frame update
    public void OnClick()
    {
        money money;
        gameobject = GameObject.Find("moneyGUI");
        money = gameobject.GetComponent<money>();
        int money1 = money.mainmoney;
        Debug.Log("押された!");
        if (money1 >= 5)
        {
  
			StatusUI.instance.ATK++ ;

        }
    }
}

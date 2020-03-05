using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInformation : MonoBehaviour
{
    public static EnemyInformation instance;
    [SerializeField]
    public Sprite blank;
    public Image MAINSPRITE;
    public Text EnemyHPText;
    public Text EnemyGOLDText;
    public Text EnemyEXPText;

    public void GetInformation(Sprite sprite,int hp,int gold,int exp)
    {

        MAINSPRITE.sprite = sprite;
        EnemyHPText.text=hp.ToString();
        EnemyGOLDText.text = gold.ToString();
        EnemyEXPText.text = exp.ToString();
    }
    public void Blank()
    {

        MAINSPRITE.sprite = blank;
        EnemyHPText.text = null;
        EnemyGOLDText.text = null;
        EnemyEXPText.text = null;
    }
    private void Awake()
    {
        instance = GetComponent<EnemyInformation>();
        Blank();
    }

}

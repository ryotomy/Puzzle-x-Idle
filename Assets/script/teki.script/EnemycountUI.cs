using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemycountUI : MonoBehaviour
{
    public static EnemycountUI instance;

    public Text EnemyLevelText;
    public int EnemyLevel=1; 
    public Text DefeatedEnemiesText;
    public int defeatedEnemies = 0;

    [SerializeField]
    public GameObject EnemyLevelPop;
    private void Awake()
    {
        instance = GetComponent<EnemycountUI>();
        DefeatedEnemiesText.text = defeatedEnemies.ToString();
        EnemyLevelText.text = EnemyLevel.ToString();
    }

    public void EnemyCount()
    {
        defeatedEnemies++;
        DefeatedEnemiesText.text = defeatedEnemies.ToString();

       
        

        if (defeatedEnemies % 5 == 0)
        {
            EnemyLevel++;
            EnemyLevelText.text = EnemyLevel.ToString();

            GameObject obj = Instantiate(EnemyLevelPop);
            obj.transform.SetParent(transform, false);
        }
    }

    private void Update()
    {
        
    }
}

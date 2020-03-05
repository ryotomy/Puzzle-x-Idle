using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class comboGUI : MonoBehaviour
{
    [SerializeField]
    public GameObject combo;

  
    public static comboGUI instance;
    void Awake()
    {
        instance = GetComponent<comboGUI>();
    }


    private void Update()
    {
       
    }


    public void spawncombo()
    {
       
        GameObject obj = Instantiate(combo);
        obj.transform.SetParent(transform,false);


    }

}

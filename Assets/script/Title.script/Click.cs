using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
	[SerializeField]
	private bool isReturning = false;
	private int a;
	private void Awake()
	{
		a = 0;
		isReturning = false;
		new WaitForSeconds(1);
	}
	void Update(){
		
			if (!isReturning) {
				if (a==1)
				{
				GameManager.instance.LoadScene("mainstage");
				isReturning = true;
				a++;
				}	
			}
		if(Input.GetMouseButtonDown(0)){
			a++;
		}
	}

}

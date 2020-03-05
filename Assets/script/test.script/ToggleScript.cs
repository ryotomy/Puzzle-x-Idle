using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleScript : MonoBehaviour
{

	Toggle toggle;

	void Start()
	{
		toggle = GetComponent<Toggle>();
	}

	void Update()
	{
		//		Debug.Log(toggle.isOn);
	}

	public void ChangeToggle()
	{
		Debug.Log("Toggleが変更されました");
	}
}

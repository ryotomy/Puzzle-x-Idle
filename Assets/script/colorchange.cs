using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class colorchange : MonoBehaviour
{

		// Start is called before the first frame update
	void Start()
    {
		GetComponent<Renderer>().material.color = Color.red;

	}

	// Update is called once per frame
	void Update()
    {
		
	}
}

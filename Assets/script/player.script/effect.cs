using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
	GameObject gameobject;
	private Animator _anim;
	[SerializeField]
	float timeElapsed = 0.0f;
	float timeOut;
	void Start()
    {
		_anim = GetComponent<Animator>();
		moveplayer moveplayer;
		_anim.Play("effect", 0, 0.0f);
		gameobject = GameObject.Find("y1");
		moveplayer = gameobject.GetComponent<moveplayer>();
		timeOut = moveplayer.timeOut;
	}

    // Update is called once per frame
    void Update()
    {
        if (StartTime.instance.pause)
        {
            _anim.speed = 0;
            return;
        }
        _anim.speed = 1f;
        timeElapsed += Time.deltaTime;

		if (timeElapsed >= timeOut)
		{

			Destroy(this.gameObject);

		}
	}
}

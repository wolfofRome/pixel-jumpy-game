using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer
	:
	MonoBehaviour
{
	void Update()
	{
		time += Time.deltaTime;
	}

	void OnTriggerEnter2D( Collider2D coll )
	{
		if( coll.tag == "Player" )
		{
			print( time );
		}
	}

	float time = 0.0f;
}

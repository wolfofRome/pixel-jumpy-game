using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike
	:
	MonoBehaviour
{
	void OnCollisionEnter2D( Collision2D coll )
	{
		if( coll.gameObject.tag == "Player" )
		{
			coll.gameObject.GetComponent<PlayerMove>()
				// ?.KnockBack( transform.position,knockback );
				?.Ouch();
		}
	}

	// [SerializeField] float knockback = 3.0f;
}

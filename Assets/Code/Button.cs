using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button
	:
	TriggerBase
{
	void OnTriggerEnter2D( Collider2D other )
	{
		if( canClick && other.tag == "Player" )
		{
			Trigger();
			canClick = false;
			GetComponent<SpriteRenderer>().sprite = Resources
				.Load<Sprite>( "Sprites/ButtonClicked" );

			if( refreshTime > 0.0f )
			{
				StartCoroutine( ClickRefresh( refreshTime ) );
			}
		}
	}

	IEnumerator ClickRefresh( float t )
	{
		canClick = false;
		yield return( new WaitForSeconds( t ) );
		canClick = true;
	}

	[Header( "How long to reset, set to 0 to disable." )]
	[SerializeField] float refreshTime = 0.0f;
	bool canClick = true;
}

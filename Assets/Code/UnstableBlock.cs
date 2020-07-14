using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableBlock
	:
	ResponderBase
{
	void Start()
	{
		rend = GetComponent<SpriteRenderer>();
		coll = GetComponent<BoxCollider2D>();
		if( standardSpr == null )
		{
			standardSpr = rend.sprite;
		}
		if( brokenSpr == null )
		{
			brokenSpr = Resources.Load<Sprite>( "Sprites/UnstableHighlight" );
		}
	}

	void Update()
	{
		if( breaking )
		{
			rend.color = Color.Lerp( rend.color,end,Time.deltaTime );
		}
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		if( !breaking && coll.gameObject.tag == "Player" )
		{
			breaking = true;
			StartCoroutine( Break( breakTime ) );
		}
	}

	IEnumerator Break( float time )
	{
		yield return( new WaitForSeconds( time ) );
		rend.sprite = brokenSpr;
		rend.color = start;
		breaking = false;
		coll.enabled = false;
	}

	public override void Trigger()
	{
		rend.sprite = standardSpr;
		rend.color = start;
		coll.enabled = true;

		// StopCoroutine( Break( 0.0f ) );
		StopAllCoroutines();
		breaking = false;
	}

	[SerializeField] float breakTime = 1.5f;
	static readonly Color start = Color.white;
	static readonly Color end = new Color( 0.5f,0.5f,0.5f,0.4f );
	static Sprite standardSpr = null;
	static Sprite brokenSpr = null;
	SpriteRenderer rend;
	BoxCollider2D coll;
	bool breaking = false;
}

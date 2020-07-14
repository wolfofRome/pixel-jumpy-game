using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove
	:
	MonoBehaviour
{
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		var xMove = Input.GetAxis( "Walk" );

		anim.SetBool( "Walking",xMove != 0.0f );
		anim.SetBool( "Jumping",vel.y > 0.002f );
		anim.SetBool( "Falling",vel.y < -0.25f );

		if( /*canJump &&*/ Mathf.Approximately( xMove,0.0f ) )
		{
			vel.x *= 0.8f;
		}

		vel.x += xMove * accel * Time.deltaTime;

		vel.y -= gravAccel * Time.deltaTime;

		if( Input.GetAxis( "Jump" ) > 0.0f )
		{
			if( canJump )
			{
				jumping = true;
				canJump = false;
				vel.y = 0.0f;
				StartCoroutine( JumpTimer( jumpTime ) );
			}
		}
		else if( jumping )
		{
			StopJump();
		}

		if( jumping )
		{
			vel.y = jumpPower;
		}

		body.MovePosition( transform.position + ( Vector3 )vel );

		if( Mathf.Abs( vel.x ) > 0.01f )
		{
			transform.localScale = new Vector3( vel.x / Mathf.Abs( vel.x ),1.0f,1.0f );
		}

		vel.x *= decay;

		if( jumpLeniency.Update( Time.deltaTime ) )
		{
			jumpLeniency.Reset();
			canJump = false;
		}
	}

	// void OnTriggerEnter2D( Collider2D coll )
	// {
	// 	vel.x *= landPenalty;
	// }

	void OnTriggerStay2D( Collider2D coll )
	{
		if( coll.tag == "Floor" )
		{
			canJump = true;
			jumpLeniency.Reset();
			if( vel.y < 0.0f ) vel.y = 0.0f;
		}
	}

	void OnCollisionEnter2D( Collision2D coll )
	{
		StopJump();
	}

	IEnumerator JumpTimer( float s )
	{
		yield return( new WaitForSeconds( s ) );
		jumping = false;
	}

	void StopJump()
	{
		jumping = false;
		vel.y *= jumpPenalty;
	}

	public void KnockBack( Vector2 src,float force )
	{
		var diff = transform.position - ( Vector3 )src;
		vel = diff.normalized * force;
	}

	public void Ouch()
	{
		PlayerPrefs.Save();
		SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
	}

	Rigidbody2D body;
	Animator anim;

	Vector2 vel = Vector2.zero;
	bool canJump = false;
	bool jumping = false;

	[SerializeField] float accel = 10.0f;
	[SerializeField] float decay = 0.94f;
	[SerializeField] float gravAccel = 1.0f;
	[SerializeField] float jumpPower = 3.0f;
	[SerializeField] float jumpTime = 0.5f;
	[SerializeField] float jumpPenalty = 0.75f;
	// [SerializeField] float landPenalty = 0.5f;
	[SerializeField] Timer jumpLeniency = new Timer( 0.2f );
}

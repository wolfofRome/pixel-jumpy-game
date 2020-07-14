using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint
	:
	MonoBehaviour
{
	void Start()
	{
		myId = GenerateId();
		flare = Instantiate( Resources.Load<GameObject>( "Prefabs/CheckpointFlare" ),
			transform.GetChild( 0 ) ).GetComponent<SpriteRenderer>();
		flare.color = invisible;
		others = FindObjectsOfType<Checkpoint>();

		if( savedId.Length < 1 ) savedId = PlayerPrefs.GetString( prefPath,"" );

		if( savedId.Length > 0 )
		{
			foreach( var other in others )
			{
				if( other.myId == savedId )
				{
					other.flare.color = visible;
					FindObjectOfType<PlayerMove>()
						.transform.position = other.transform.position;
				}
			}
		}
	}

	void Update()
	{
		// Reset save.
		if( Input.GetKey( KeyCode.R ) )
		{
			savedId = "";
			PlayerPrefs.SetString( prefPath,savedId );
			SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
		}
	}

	void OnTriggerEnter2D( Collider2D coll )
	{
		if( myId != savedId && coll.tag == "Player" )
		{
			savedId = myId;
			PlayerPrefs.SetString( prefPath,savedId );

			foreach( var other in others )
			{
				other.flare.color = invisible;
			}

			flare.color = visible;
		}
	}

	string GenerateId()
	{
		return( transform.position.ToString() );
	}

	static string savedId = "";
	string myId;
	SpriteRenderer flare;
	Checkpoint[] others;
	static Color invisible = new Color( 1.0f,1.0f,1.0f,0.0f );
	static Color visible = new Color( 1.0f,1.0f,1.0f,1.0f );
	public static string prefPath = "save";
}

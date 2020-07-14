using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit
	:
	MonoBehaviour
{
	void OnTriggerEnter2D( Collider2D coll )
	{
		if( coll.tag == "Player" )
		{
			PlayerPrefs.SetString( Checkpoint.prefPath,"" );
			SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
		}
	}
}

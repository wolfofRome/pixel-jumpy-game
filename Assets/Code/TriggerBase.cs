using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBase
	:
	MonoBehaviour
{
	protected void Trigger()
	{
		triggerees.RemoveAll( responder => responder == null );

		// foreach( var other in triggerees )
		for( int i = 0; i < triggerees.Count; ++i )
		{
			StartCoroutine( BeginTrigger( triggerees[i],triggerSpread * i ) );
		}
	}

	IEnumerator BeginTrigger( ResponderBase responder,float t )
	{
		yield return( new WaitForSeconds( t ) );
		responder.Trigger();
	}

	[SerializeField] List<ResponderBase> triggerees = new List<ResponderBase>();
	[SerializeField] float triggerSpread = 0.5f;
}

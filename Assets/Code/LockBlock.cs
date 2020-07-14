using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBlock
	:
	ResponderBase
{
	public override void Trigger()
	{
		if( gameObject != null ) Destroy( gameObject );
	}
}

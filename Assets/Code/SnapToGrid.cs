using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapToGrid
	:
	MonoBehaviour
{
	void Start()
	{
		if( Application.isPlaying ) Destroy( this );
	}

	void Update()
	{
		var pos = transform.position;
		pos.Set(
			Mathf.Round( pos.x / snap ) * snap,
			Mathf.Round( pos.y / snap ) * snap,
			0.0f );
		transform.position = pos;

		var rot = transform.eulerAngles;
		rot.Set( rot.x,rot.y,Mathf.Round( rot.z / 90.0f ) * 90.0f );
		transform.eulerAngles = rot;
	}

	const float snap = 0.5f;
}
using UnityEngine;
using System.Collections;

public class MainCharacter : Movable
{
	public float Speed = 10f;
	[Range(0f,1f)]
	public float Friction = 1f;

	public override void Update () 
	{
		ProcessInput();
		base.Update();
	}

	private void ProcessInput() 
	{
		Vector3 axis = Vector3.zero;
		bool move = false;

		if(Input.GetKey(KeyCode.UpArrow)) 
		{
			axis += Vector3.up;
			move = true;
		}

		if(Input.GetKey(KeyCode.DownArrow)) 
		{
			axis += Vector3.down;
			move = true;
		}

		if(Input.GetKey(KeyCode.LeftArrow)) 
		{
			axis += Vector3.left;
			move = true;
		}

		if(Input.GetKey(KeyCode.RightArrow)) 
		{
			axis += Vector3.right;
			move = true;
		}

		if(move)
		{
			axis.Normalize();
			Move(axis, Speed, Friction);
		}
	}
}

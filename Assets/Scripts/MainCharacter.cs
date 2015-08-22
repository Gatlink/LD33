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
		float speed = Speed;

		if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
		{
			Debug.Log("meh");
			move = true;
			axis.x = Input.GetAxis("Horizontal");
			axis.y = Input.GetAxis("Vertical");
			speed *= Mathf.Max(Mathf.Abs(axis.x), Mathf.Abs(axis.y));
			Debug.Log(speed);
		}
		else 
		{
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
		}

		if(move)
		{
			Move(axis, speed, Friction);
		}
	}
}

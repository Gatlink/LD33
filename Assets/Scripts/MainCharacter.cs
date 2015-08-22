using UnityEngine;
using System.Collections;

public class MainCharacter : Movable
{
	public Transform Target;
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
		var scale = transform.localScale;
		scale.x = Target.position.x < transform.position.x ? -1 : 1;
		transform.localScale = scale;

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

		// if (Input.GetAxis("Fire 1"))
		// {
			
		// }

		if(move)
		{
			Move(axis, speed, Friction);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Monster : Movable
{
	public Transform Target;
	public float Speed = 1.6f;
	public float VelocityGain = 10f;
	public float PositionGain = 50f;
	public float DistanceMinFromTarget = 1f;
	public float DistanceMaxFromTarget = 3f;

	public override void Awake()
	{
		_velocity = Vector3.zero;
		_friction = 1f;
	}

	public override void Update()
	{
		// Turn toward the player
		var scale = transform.localScale;
		scale.x = Target.position.x < transform.position.x ? -1 : 1;
		transform.localScale = scale;

		var positionError = Target.position - transform.position;
		var distance = positionError.magnitude;

		if (Moving && distance <= DistanceMinFromTarget)
		{
			_velocity = Vector3.zero;
			return;
		}

		if (!Moving && distance <= DistanceMaxFromTarget)
			return;
		
		var velocityError = -_velocity;
		var acc = Speed * (positionError * PositionGain + velocityError * VelocityGain);
		_velocity += acc * Time.deltaTime;

		base.Update();
	}
}
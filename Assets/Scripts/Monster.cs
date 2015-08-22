using UnityEngine;
using System.Collections;

public class Monster : Movable
{
	public Transform Player;
	public float SpeedRatio = 1.6f;
	public float VelocityGain = 10f;
	public float PositionGain = 50f;
	public float DistanceMinFromTarget = 1f;
	public float DistanceMaxFromTarget = 3f;

	public override void Awake()
	{
		_velocity = Vector3.zero;
	}

	public override void Update()
	{
		// Turn toward the player
		var scale = transform.localScale;
		scale.x = Player.position.x < transform.position.x ? -1 : 1;
		transform.localScale = scale;

		var positionError = Player.position - transform.position;
		var inRange = (positionError.sqrMagnitude <= DistanceMinFromTarget * DistanceMinFromTarget);

		if (Moving && inRange)
		{
			_velocity = Vector3.zero;
			return;
		}

		if (!Moving && inRange)
			return;
		
		var velocityError = -_velocity;
		var acc = SpeedRatio * (positionError * PositionGain + velocityError * VelocityGain);
		_velocity += acc * Time.deltaTime;

		base.Update();
	}
}
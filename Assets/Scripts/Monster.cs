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
		m_velocity = Vector3.zero;
		m_friction = 1f;
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
			m_velocity = Vector3.zero;
			return;
		}

		if (!Moving && distance <= DistanceMaxFromTarget)
			return;
		
		var velocityError = -m_velocity;
		var acc = Speed * (positionError * PositionGain + velocityError * VelocityGain);
		m_velocity += acc * Time.deltaTime;

		base.Update();
	}
}
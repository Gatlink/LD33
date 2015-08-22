using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour 
{
	public float MaxSpeed = 600f;
	[Range(0f,1f)]
	public float Acceleration = 1f;
	[Range(0f,1f)]
	public float Friction = 1f;

	protected Vector3 _velocity;
	protected Vector3 _acceleration;

	public bool Moving 
	{
		get 
		{
			return _velocity.sqrMagnitude > 0f;
		}
	}

	public float Speed
	{
		get 
		{
			return _velocity.magnitude;
		}
	}

	public virtual void Awake() 
	{
		_velocity = Vector3.zero;
	}

	public virtual void Update()
	{
		if(Moving) 
		{
			transform.Translate(_velocity * Time.deltaTime);

			_velocity += _acceleration;
			Vector3.ClampMagnitude(_velocity, MaxSpeed);
			_acceleration *= 1f - Friction;
			_velocity *= 1f - Friction;

			var scale = transform.localScale;
			scale.x = _velocity.x < 0f ? -1 : 1;
			transform.localScale = scale;
		}
	}
	
	public void Move(Vector3 axis) 
	{
		float thrust = MaxSpeed * Mathf.Max(Mathf.Abs(axis.x), Mathf.Abs(axis.y));
		axis.Normalize();
			
		_acceleration = (axis * thrust) * Acceleration;
		if(!Moving)
		{
			_velocity = _acceleration;
		}
	}
}

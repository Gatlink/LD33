using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour 
{
	protected Vector3 _velocity;
	protected float _friction;

	public bool Moving 
	{
		get 
		{
			return _velocity.sqrMagnitude > 0f;
		}
	}

	public virtual void Awake() 
	{
		_velocity = Vector3.zero;
		_friction = 0f;
	}

	public virtual void Update()
	{
		if(Moving) 
		{
			transform.Translate(_velocity * Time.deltaTime);
			_velocity *= _friction;
		}
	}
	
	public void Move(Vector3 axis, float thrust, float friction) 
	{
		axis.Normalize();
		_velocity = axis * thrust;
		_friction = 1f - Mathf.Clamp01(friction);
	}
}

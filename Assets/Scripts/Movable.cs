using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour 
{	
	protected Vector3 _velocity;

	public float Speed = 600f;
	[Range(0f,1f)]
	public float Friction = 1f;

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
	}

	public virtual void Update()
	{
		if(Moving) 
		{
			transform.Translate(_velocity * Time.deltaTime);
			_velocity *= 1f - Friction;

			var scale = transform.localScale;
			scale.x = _velocity.x < 0f ? -1 : 1;
			transform.localScale = scale;
		}
	}
	
	public void Move(Vector3 axis) 
	{
		float thrust = Speed * Mathf.Max(Mathf.Abs(axis.x), Mathf.Abs(axis.y));
		axis.Normalize();
		_velocity = axis * thrust;
	}
}

using UnityEngine;
using System.Collections;

public class Mobile : MonoBehaviour 
{
	[Space(5)]
	public float Speed = 1000f;

	protected Rigidbody2D _rigidbody;

	public bool Moving 
	{
		get 
		{
			return ((_rigidbody != null) ? _rigidbody.velocity.sqrMagnitude > 0f : false);
		}
	}

	public virtual void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	public void Move(Vector3 axisRatio) 
	{
		_rigidbody.AddForce(transform.right * Speed * axisRatio.x);
		_rigidbody.AddForce(transform.up * Speed * axisRatio.y);

		var scale = transform.localScale;
		scale.x = axisRatio.x < 0f ? -1f : 1f;
		transform.localScale = scale;
	}
}

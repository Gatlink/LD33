using UnityEngine;
using System.Collections;
using Tiled2Unity;

public class Mobile : MonoBehaviour 
{
	[Space(5)]
	public float Speed = 1000f;

	protected Rigidbody2D _rigidbody;

	private Transform _map;

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
		_map = GameObject.FindObjectOfType<TiledMap>().transform;
	}
	
	public virtual void Update()
	{
		var position = transform.position;
		var r = (position.y + _map.position.y) / (_map.position.y * 2);
		position.z = Mathf.Lerp(-100, 100, r);
		transform.position = position;
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

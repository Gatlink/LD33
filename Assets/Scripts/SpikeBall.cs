using UnityEngine;
using System.Collections;

public class SpikeBall : Poolable 
{
	public float Speed = 1200f;
	public float Lifetime = 2f;
	public bool Bouncing = false;

	private int _damage;
	private float _elapsed;

	private Rigidbody2D _rigidbody;

	public void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		Reset();
	}

	public void Update()
	{
		_elapsed += Time.deltaTime;
		if(_elapsed > Lifetime)
		{
			SpikeBallPool.Instance.Recycle(this);
		}
	}

	public void Fire(Vector3 origin, Vector2 direction, int damageValue)
	{
		transform.position = origin;

		_damage = damageValue;
		_elapsed = 0f;

		direction.Normalize();
		_rigidbody.AddForce(direction * Speed/*, ForceMode2D.Impulse*/);
	}

	public override void Reset()
	{
		_damage = 0;
		_elapsed = 0f;
	}
}

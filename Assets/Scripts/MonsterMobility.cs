using UnityEngine;
using System.Collections;

public class MonsterMobility : MonoBehaviour
{
	public Transform Player;
	public float Speed = 10000f;
	public float DistanceMaxFromPlayer = 85f;

	private Rigidbody2D _rigidbody;

	public void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	public void FixedUpdate()
	{
		var direction = Player.position - transform.position;
		var distance = direction.sqrMagnitude;
		direction.Normalize();
		_rigidbody.AddForce(direction * Speed);

		var scale = transform.localScale;
		scale.x = Player.position.x < transform.position.x ? -1 : 1;
		transform.localScale = scale;
	}
}

using UnityEngine;
using System.Collections;

public class Monster : Mobile
{
	[Space(5)]
	public Transform Player;
	public float DistanceMinFromPlayer = 128f;

	public virtual void FixedUpdate()
	{
		var direction = Player.position - transform.position;
		var sqrdDist = direction.sqrMagnitude;
		var r = sqrdDist - (DistanceMinFromPlayer * DistanceMinFromPlayer);
		direction.Normalize();
		direction *= Mathf.Lerp(0, 1f, Mathf.Clamp01(r));
		Move(direction);

		// Turn toward the player
		var scale = transform.localScale;
		scale.x = Player.position.x < transform.position.x ? -1f : 1f;
		transform.localScale = scale;
	}

	public void GetKicked(Vector3 force)
	{
		_rigidbody.AddForce(force);
	}
}
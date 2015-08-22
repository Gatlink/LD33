using UnityEngine;
using System.Collections;

public class Monster : Movable
{
	public Transform Player;

	public float DistanceMinFromTarget = 128f;
	public float DistanceMaxFromTarget = 320f;

	public bool Following
	{
		get; private set;
	} 

	public override void Update()
	{
		// Turn toward the player
		var scale = transform.localScale;
		scale.x = Player.position.x < transform.position.x ? -1 : 1;
		transform.localScale = scale;

		var toTarget = Player.position - transform.position;
		var closeEnough = (toTarget.sqrMagnitude <= DistanceMinFromTarget * DistanceMinFromTarget);
		if(closeEnough)
		{
			Following = false;
			return;
		}
		
		var tooFar = (toTarget.sqrMagnitude >= DistanceMaxFromTarget * DistanceMaxFromTarget);
		if(tooFar || Following)
		{
			Following = true;
			toTarget.Normalize();
			Move(toTarget);
		}

		base.Update();
	}
}
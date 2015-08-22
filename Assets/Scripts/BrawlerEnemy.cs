using UnityEngine;
using System.Collections;

public class BrawlerEnemy : Enemy {
	public override void Acquire()
	{
		Tracking = true;
		Vector3 toTarget = MainCharacter.Instance.transform.position - transform.position;
		if(toTarget.sqrMagnitude < 32f * 32f)
		{
			Attack();
		}
		else
		{
			Move(toTarget);
		}
	}

	public override void Attack() 
	{
		if(MainCharacter.Instance.Alive)
		{
			MainCharacter.Instance.Hurt(Damage);
		}
	}

	public override void Die()
	{
		Debug.Log(name + " died");
	}
}

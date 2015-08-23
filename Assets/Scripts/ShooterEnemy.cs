using UnityEngine;
using System.Collections;

public class ShooterEnemy : Enemy 
{
	protected override void Acquire()
	{
		Tracking = true;
		Vector3 toTarget = MainCharacter.Instance.transform.position - transform.position;
		bool inRange = toTarget.sqrMagnitude <= AttackRange * AttackRange;
		
		if(inRange)
		{
			Attack();
		}
		else
		{
			//TODO find a point at distance from target
			//toTarget.Normalize();
			//Move(toTarget);
		}
	}

	public override void Attack() 
	{
		if(MainCharacter.Instance.Alive && CanAttack)
		{
			CanAttack = false;
			StartCoroutine(CoolWeaponDown());
			SpikeBall bullet = SpikeBallPool.Instance.GetNext(true);
			
			Vector3 toTarget = MainCharacter.Instance.transform.position - transform.position;
			bullet.Fire(transform.position, (Vector2)toTarget, Damage);
			//MainCharacter.Instance.Hurt(Damage);
		}
	}
}

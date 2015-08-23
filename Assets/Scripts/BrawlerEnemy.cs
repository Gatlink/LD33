﻿using UnityEngine;
using System.Collections;

public class BrawlerEnemy : Enemy 
{
	protected override void Acquire()
	{
		Tracking = true;
		Vector3 toTarget = MainCharacter.Instance.transform.position - transform.position;
		if(toTarget.sqrMagnitude < 64f * 64f)
		{
			Attack();
		}
		else
		{
			toTarget.Normalize();
			Move(toTarget);
		}
	}

	public override void Attack() 
	{
		if(MainCharacter.Instance.Alive && CanAttack)
		{
			CanAttack = false;
			StartCoroutine(CoolWeaponDown());
			MainCharacter.Instance.Hurt(Damage);
		}
	}
}

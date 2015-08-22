﻿using UnityEngine;
using System.Collections;

public abstract class Character : Movable {
	[Space(5)]
	public int Life = 50;
	public int Damage = 12;

	public bool Dead 
	{
		get { return Life <= 0; }
	}

	public bool Alive 
	{
		get { return !Dead; }
	}
	
	public abstract void Attack();
	public abstract void Die();

	public void Hurt(int damage) 
	{
		Life -= damage;
		if(Life <= 0)
		{
			Life = 0;
			Die();
		}
	}
}

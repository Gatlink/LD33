using UnityEngine;
using System.Collections;

public abstract class Enemy : Character {
	public float AcquisitionRange = 150f;

	public bool Tracking 
	{
		get; protected set;
	}

	public override void Update()
	{
		float distFromHero = (MainCharacter.Instance.transform.position - transform.position).sqrMagnitude;
		if(distFromHero <= AcquisitionRange * AcquisitionRange || Tracking) 
		{
			Acquire();
		}

		base.Update();
	}

	public abstract void Acquire();
}

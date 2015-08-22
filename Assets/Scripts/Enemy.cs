using UnityEngine;
using System.Collections;

public abstract class Enemy : Character {
	public float AcquisitionRange = 150f;

	[Space(5)]
	[Range(0f,5f)]
	public float MinRandomDelay = 1f;
	[Range(1f,10f)]
	public float MaxRandomDelay = 3f;

	public bool Tracking 
	{
		get; protected set;
	}

	public override void Update()
	{
		if(Dead)
			return;

		float distFromHero = (MainCharacter.Instance.transform.position - transform.position).sqrMagnitude;
		if(distFromHero <= AcquisitionRange * AcquisitionRange || Tracking) 
		{
			Acquire();
			StopAllCoroutines();
		}
		else if(!Moving)
		{
			MoveRandomly();
		}

		base.Update();
	}

	protected abstract void Acquire();

	protected void MoveRandomly()
	{
		float duration = Random.Range(MinRandomDelay, MaxRandomDelay);
		Vector3 dir = Random.insideUnitCircle;
		dir.Normalize();
		StartCoroutine(KeepMoving(duration, dir));
	}

	private IEnumerator KeepMoving(float duration, Vector3 direction) 
	{
		float start = Time.time;
		float elapsed = 0f;
		while(elapsed < duration) {
			elapsed = Time.time - start;
			Move(direction);
			yield return 0;
		}
	}
}

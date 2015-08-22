using UnityEngine;
using System.Collections;

public abstract class Enemy : Character 
{
	public float AcquisitionRange = 150f;

	public float AttackCooldown = 1f;

	[Space(5)]
	[Range(0f,5f)]
	public float MinRandomDelay = 1f;
	[Range(1f,10f)]
	public float MaxRandomDelay = 3f;

	private Coroutine _randomMovement = null;

	public bool Tracking 
	{
		get; protected set;
	}

	public bool CanAttack
	{
		get; protected set;
	}

	public virtual void Awake()
	{
		CanAttack = true;
		_randomMovement = null;
	}

	public virtual void Update()
	{
		if(Dead)
			return;

		float distFromHero = (MainCharacter.Instance.transform.position - transform.position).sqrMagnitude;
		if(distFromHero <= AcquisitionRange * AcquisitionRange || Tracking) 
		{
			Acquire();
			if(_randomMovement != null)
			{
				StopCoroutine(_randomMovement);
				_randomMovement = null;
			}
		}
		else if(!Moving)
		{
			MoveRandomly();
		}
	}

	protected abstract void Acquire();

	protected void MoveRandomly()
	{
		float duration = Random.Range(MinRandomDelay, MaxRandomDelay);
		Vector3 dir = Random.insideUnitCircle;
		dir.Normalize();
		_randomMovement = StartCoroutine(KeepMoving(duration, dir));
	}

	protected IEnumerator KeepMoving(float duration, Vector3 direction) 
	{
		float start = Time.time;
		float elapsed = 0f;
		while(elapsed < duration) {
			elapsed = Time.time - start;
			Move(direction);
			yield return 0;
		}
	}

	protected IEnumerator CoolWeaponDown() 
	{
		float start = Time.time;
		float elapsed = 0f;
		while(elapsed < AttackCooldown) {
			elapsed = Time.time - start;
			yield return 0;
		}
		CanAttack = true;
	}
}

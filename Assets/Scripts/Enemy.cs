using UnityEngine;
using System.Collections;

public abstract class Enemy : Character 
{
	public float AcquisitionRange = 150f;
	public float AttackCooldown = 1f;
	public int MinVelocityToGetHurt = 200;

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

	public override void Update()
	{
		if(Dead)
			return;

		float distFromHero = (MainCharacter.Instance.transform.position - transform.position).sqrMagnitude;
		if(distFromHero <= AcquisitionRange * AcquisitionRange || Tracking) 
		{
			Acquire();
			InterruptRandomMovement();
		}
		else if(_randomMovement == null)
		{
			_randomMovement = StartCoroutine(MoveRandomly());
		}
	}

	protected abstract void Acquire();

	protected IEnumerator MoveRandomly()
	{
		float duration = Random.Range(MinRandomDelay, MaxRandomDelay);
		Vector3 dir = Random.insideUnitCircle;
		dir.Normalize();
		
		float start = Time.time;
		float elapsed = 0f;
		while(elapsed < duration) {
			elapsed = Time.time - start;
			Move(dir);
			yield return 0;
		}
		_randomMovement = null;
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

	public void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Monster")
		{
			var damage = (int) col.rigidbody.velocity.magnitude;
			if (damage >= MinVelocityToGetHurt)
				Hurt(damage);
		}
	}

	public override void Die()
	{
		GetComponent<SpriteRenderer>().color = Color.red;
		InterruptRandomMovement();
	}

	protected void InterruptRandomMovement()
	{
		if(_randomMovement != null)
		{
			StopCoroutine(_randomMovement);
			_randomMovement = null;
		}
	}
}

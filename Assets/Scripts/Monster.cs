using UnityEngine;
using System.Collections;
using Tiled2Unity;

public class Monster : Mobile
{
	private enum State 
	{
		IDLE
		, SHOT
		, FOLLOWING
	};

	[Space(5)]
	public Transform Player;
	public float DistanceMinFromPlayer = 128f;

	private State _currentState = State.IDLE;
	private Animator _anim;

	public override void Start()
	{
		base.Start();
		Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		_anim = GetComponent<Animator>();
	}

	public virtual void FixedUpdate()
	{
		if(_currentState == State.IDLE || _currentState == State.FOLLOWING)
		{
			var direction = Player.position - transform.position;
			var sqrdDist = direction.sqrMagnitude;
			var sqrMinDist = DistanceMinFromPlayer * DistanceMinFromPlayer;

			bool tooFar = sqrdDist > sqrMinDist;
			if(tooFar)
			{
				var speed = Mathf.Clamp01(sqrdDist - sqrMinDist);
				direction.Normalize();
				direction *= speed;
				_anim.SetFloat("Speed", speed);
				Move(direction);				
			}
			_currentState = tooFar ? State.FOLLOWING : State.IDLE;
		}
		else if (_currentState == State.SHOT && _rigidbody.velocity == Vector2.zero) 
			_currentState = State.IDLE;

		// Turn toward the player
		var scale = transform.localScale;
		scale.x = Player.position.x < transform.position.x ? -1f : 1f;
		transform.localScale = scale;

		if(_rigidbody.IsSleeping())
		{
			_currentState = State.IDLE;
		}
	}

	public void GetKicked(Vector3 force)
	{
		_rigidbody.AddForce(force);
		_currentState = State.SHOT;
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.tag == "Ennemy")
		{
			_rigidbody.velocity = Vector2.zero;
		}
	}
}
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
	public float KickedDrag = 5f;
	
	private State _currentState = State.IDLE;
	private Animator _anim;
	private float _drag;

	public override void Start()
	{
		base.Start();
		Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		_anim = GetComponent<Animator>();
		_drag = _rigidbody.drag;
	}

	public virtual void FixedUpdate()
	{
		var speed = 0f;

		if(_currentState == State.IDLE || _currentState == State.FOLLOWING)
		{
			var direction = Player.position - transform.position;
			var sqrdDist = direction.sqrMagnitude;
			var sqrMinDist = DistanceMinFromPlayer * DistanceMinFromPlayer;

			bool tooFar = sqrdDist > sqrMinDist;
			if(tooFar)
			{
				speed = Mathf.Clamp01(sqrdDist - sqrMinDist);
				direction.Normalize();
				direction *= speed;
				Move(direction);				
			}
			_currentState = tooFar ? State.FOLLOWING : State.IDLE;

			// Turn toward the player
			var scale = transform.localScale;
			scale.x = Player.position.x < transform.position.x ? -1f : 1f;
			transform.localScale = scale;
		}
		else if (_currentState == State.SHOT && _rigidbody.velocity == Vector2.zero) 
		{
			_currentState = State.IDLE;
			_rigidbody.drag = _drag;
		}

		_anim.SetBool("Stopped", _rigidbody.velocity == Vector2.zero);

		if(_rigidbody.IsSleeping())
		{
			_currentState = State.IDLE;
		}
	}

	public void GetKicked(Vector3 force)
	{
		_rigidbody.AddForce(force);
		_currentState = State.SHOT;
		_anim.SetTrigger("Kicked");
		_rigidbody.drag = KickedDrag;

		var scale = transform.localScale;
		scale.x = -scale.x;
		transform.localScale = scale;
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if(_currentState == State.SHOT && collision.collider.tag == "Ennemy")
		{
			var damage = (int) _rigidbody.velocity.magnitude;
			collision.collider.GetComponent<Enemy>().Hurt(damage);
			_rigidbody.velocity = Vector2.zero;
		}
	}
}
using UnityEngine;
using System.Collections;

public class PlayerMobility : MonoBehaviour
{
	public Transform Cursor;
	public float Speed = 40f;
	public float KickStrength = 20000f;

	private Rigidbody2D _rigidbody;

	public void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	public void FixedUpdate()
	{
		var inputX = Input.GetAxis("Horizontal");
		var inputY = Input.GetAxis("Vertical");

		_rigidbody.AddForce(transform.right * Speed * inputX);
		_rigidbody.AddForce(transform.up * Speed * inputY);

		var scale = transform.localScale;
		scale.x = Cursor.position.x < transform.position.x ? -1 : 1;
		transform.localScale = scale;

		if (Input.GetAxis("Fire1") != 0f)
			Attack();
	}

	public void Attack() 
	{
		var direction = (Cursor.position - transform.position).normalized;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 64f, 1 << LayerMask.NameToLayer("Monster"));
		if (hit.collider != null)
		{
			Debug.Log("PAF");
			var monster = hit.collider.GetComponent<MonsterMobility>();
			monster.GetKicked(direction * KickStrength);
		}
	}
}
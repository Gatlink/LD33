﻿using UnityEngine;
using System.Collections;

public class PlayerMobility : MonoBehaviour
{
	public Transform Cursor;
	public float Speed = 40f;

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
	}
}
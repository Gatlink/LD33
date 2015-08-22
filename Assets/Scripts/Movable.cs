using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour {
	protected Vector3 m_velocity;
	protected float m_friction;

	public bool Moving 
	{
		get 
		{
			return m_velocity.sqrMagnitude > 0f;
		}
	}

	public virtual void Awake() 
	{
		m_velocity = Vector3.zero;
		m_friction = 0f;
	}

	public virtual void Update()
	{
		if(Moving) 
		{
			transform.Translate(m_velocity * Time.deltaTime);
			m_velocity *= m_friction;
		}
	}
	
	public void Move(Vector3 axis, float thrust, float friction) 
	{
		axis.Normalize();
		m_velocity = axis * thrust;
		m_friction = 1f - Mathf.Clamp01(friction);
	}

}

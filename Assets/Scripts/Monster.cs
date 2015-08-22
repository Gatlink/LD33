using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
	public void GetKicked(Vector3 force)
	{
		GetComponent<Rigidbody2D>().AddForce(force);
	}
}
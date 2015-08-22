using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour 
{
	public Transform Target;

	void Update() 
	{
		Vector3 pos = Target.transform.position;
		pos.z = transform.position.z;
		transform.position = pos;
	}
}

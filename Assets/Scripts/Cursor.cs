using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour
{
	public void Update()
	{
		var mousePos = Input.mousePosition;
		mousePos.z = 10;
		transform.position = Camera.main.ScreenToWorldPoint(mousePos);
	}
}
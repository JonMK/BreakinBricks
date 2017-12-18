using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ball") 
			GameManager.Instance.LoseLife ();
	}
}

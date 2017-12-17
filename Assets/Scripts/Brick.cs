using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	public GameObject paritcles;

	void OnCollisionEnter2D(Collision2D coll) 
	{
//		if (coll.gameObject.tag == "Enemy")
//			coll.gameObject.SendMessage("ApplyDamage", 10);

		//Instantiate(paritcles, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}

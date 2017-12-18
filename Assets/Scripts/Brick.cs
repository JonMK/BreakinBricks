using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	public GameObject brickDeathParticles;
	public GameObject ballHitParticles;

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "Ball") 
		{
			//Instantiate(brickDeathParticles, transform.position, Quaternion.identity);
		
			//Invoke ("Death", .7f);
			Death();
		}
	}

	private void Death()
	{
		//Instantiate(brickDeathParticles, transform.position, Quaternion.identity);

		gameObject.SetActive (false);

		GameManager.Instance.BrickDeath ();
	}
}

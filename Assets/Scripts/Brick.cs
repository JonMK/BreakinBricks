using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	public ParticleSystem brickBurstParticlesPrefab;
	public ParticleSystem ballHitParticlesPrefab;

	private ParticleSystem _brickBurstParticles;

	void Awake()
	{
		_brickBurstParticles = Instantiate(brickBurstParticlesPrefab, transform.position, Quaternion.identity) as ParticleSystem;
		//Instantiate(ballHitParticles, transform.position, Quaternion.identity);
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "Ball") 
		{
			//ballHitParticles.Play ();
		
			//Invoke ("Death", .7f);
			Death();
		}
	}

	private void Death()
	{
		_brickBurstParticles.Play ();

		gameObject.SetActive (false);

		GameManager.Instance.BrickDeath ();
	}
}

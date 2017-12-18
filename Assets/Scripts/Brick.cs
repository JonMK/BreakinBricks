using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	public ParticleSystem brickBurstParticlesPrefab;

	private ParticleSystem _brickBurstParticles;

	void Awake()
	{
		_brickBurstParticles = Instantiate(brickBurstParticlesPrefab, transform.position, Quaternion.identity) as ParticleSystem;
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{		
		AudioManager.Instance.PlayBrickDeath ();

		_brickBurstParticles.Play ();

		gameObject.SetActive (false);

		GameManager.Instance.BrickDeath ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
	public AudioSource audioSource;

	public AudioClip ballBounce;
	public AudioClip brickDeath;
	public AudioClip win;
	public AudioClip gameover;

	public static AudioManager Instance = null;

	private float _volLowRange = .5f;
	private float _volHighRange = 1.0f;

	private void Awake ()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy (gameObject);
	}

	public void PlayBallBounce()
	{
		float vol = Random.Range (_volLowRange, _volHighRange);
		audioSource.PlayOneShot (ballBounce, vol);
	}

	public void PlayBrickDeath()
	{
		float vol = Random.Range (_volLowRange, _volHighRange);
		audioSource.PlayOneShot (brickDeath, vol);
	}

	public void PlayWin()
	{
		audioSource.PlayOneShot (win);
	}

	public void PlayGameOver()
	{
		audioSource.PlayOneShot (gameover);
	}
}

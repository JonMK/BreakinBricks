using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public delegate void GameEvent();
	public static event GameEvent OnLeftTouch;
	public static event GameEvent OnRightTouch;
	public static event GameEvent OnTouchRelease;

	public int lives = 3;
	public float resetDelay = 1f;
	public Text livesText;

	public GameObject gameOver;
	public GameObject youWon;

	public GameObject ballPrefab;
	public GameObject paddlePrefab;

	public static GameManager Instance = null;

	private GameObject _paddle;
	private GameObject _ball;

	private void Awake () 
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy (gameObject);

		Setup();
	}

	private void Setup()
	{
		InitGameObjects ();

		// TODO : Init brick manager
	}

	private void InitGameObjects()
	{
		_paddle = Instantiate(paddlePrefab, paddlePrefab.transform.position, Quaternion.identity) as GameObject;
		_ball = Instantiate(ballPrefab, ballPrefab.transform.position, Quaternion.identity) as GameObject;
	}

	private	void CheckGameOver()
	{
		if (lives < 1) {
			Debug.Log ("LOSE");
//			gameOver.SetActive(true);
//			Time.timeScale = .25f;
			//			Invoke ("FullReset", resetDelay);
		} else
			Reset ();
	}

	private void FullReset()
	{
		// TODO
	}

	private void Reset()
	{
		TouchRelease ();

		Destroy (_paddle);
		Destroy (_ball);

		InitGameObjects ();
	}

	public void LoseLife()
	{
		lives--;
		Debug.Log ("Lives = " + lives);

		//		livesText.text = "Lives: " + lives;
		//		Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
		//		Destroy(clonePaddle);
		//		Invoke ("SetupPaddle", resetDelay);
		CheckGameOver();
	}

	public void LeftTouch()
	{
		if(OnLeftTouch != null)
			OnLeftTouch ();
	}

	public void RightTouch()
	{
		if(OnRightTouch != null)
			OnRightTouch ();
	}

	public void TouchRelease()
	{
		if(OnTouchRelease != null)
			OnTouchRelease ();
	}
}
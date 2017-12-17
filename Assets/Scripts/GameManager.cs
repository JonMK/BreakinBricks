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
	public static event GameEvent OnFullReset;

	public int lives = 3;
	public float resetDelay = 1f;

	public Text livesText;
	public Text gameOver;

	public GameObject ballPrefab;
	public GameObject paddlePrefab;

	public static GameManager Instance = null;

	private GameObject _paddle;
	private GameObject _ball;

	private bool _isGameOver;

	private int _currentLives;
	private int CurrentLives
	{
		get { return _currentLives; }
		set
		{
			if (value < 0)
				value = 0;
						
			_currentLives = value;

			livesText.text = "Lives : " + _currentLives;

			Debug.Log ("Lives = " + CurrentLives);
		}
	}

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
		_isGameOver = false;
		CurrentLives = lives;
		InitGameObjects ();
		gameOver.gameObject.SetActive (false);
	}

	private void InitGameObjects()
	{
		_paddle = Instantiate(paddlePrefab, paddlePrefab.transform.position, Quaternion.identity) as GameObject;
		_ball = Instantiate(ballPrefab, ballPrefab.transform.position, Quaternion.identity) as GameObject;
	}

	private	void CheckGameOver()
	{
		if (CurrentLives < 1) 
		{
			Debug.Log ("LOSE");

			TouchRelease ();
			Destroy (_paddle);
			Destroy (_ball);

			gameOver.text = "Game Over";
			gameOver.gameObject.SetActive (true);

			_isGameOver = true;		
		} 
		else
			Reset ();
	}

	private void FullReset()
	{
		if(OnFullReset != null)
			OnFullReset ();

		Setup ();
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
		CurrentLives--;

		CheckGameOver();
	}

	public void LeftTouch()
	{
		if (_isGameOver) 
		{
			FullReset ();
			return;
		}

		if(OnLeftTouch != null)
			OnLeftTouch ();
	}

	public void RightTouch()
	{
		if (_isGameOver) 
		{
			FullReset ();
			return;
		}

		if(OnRightTouch != null)
			OnRightTouch ();
	}

	public void TouchRelease()
	{
		if(OnTouchRelease != null)
			OnTouchRelease ();
	}
}
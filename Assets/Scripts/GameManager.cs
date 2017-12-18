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
	public int rowsOfBricks = 3;

	public Text livesText;
	public Text gameOver;

	public GameObject ballPrefab;
	public GameObject paddlePrefab;
	public BrickManager brickManagerPrefab;

	public static GameManager Instance = null;

	private GameObject _paddle;
	private GameObject _ball;

	private bool _isGameOver;

	private int _numberOfBricks;
	private int _currentNumberOfBricks;

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

		var brickManager = Instantiate(brickManagerPrefab, transform.position, Quaternion.identity) as BrickManager;
		_numberOfBricks = brickManager.InitBricks (rowsOfBricks);

		Setup();
	}

	private void Setup()
	{
		_isGameOver = false;
		CurrentLives = lives;
		_currentNumberOfBricks = _numberOfBricks;
		InitGameObjects ();
		gameOver.gameObject.SetActive (false);
	}

	private void InitGameObjects()
	{
		_paddle = Instantiate(paddlePrefab, paddlePrefab.transform.position, Quaternion.identity) as GameObject;
		_ball = Instantiate(ballPrefab, ballPrefab.transform.position, Quaternion.identity) as GameObject;
	}

	public void CheckWin ()
	{
		if (lives > 0 && _currentNumberOfBricks < 1)
		{
			AudioManager.Instance.PlayWin ();

			gameOver.text = "You Win";
			gameOver.gameObject.SetActive (true);

			StopGame ();

			_isGameOver = true;	
		}
	}

	private	void CheckGameOver()
	{
		if (CurrentLives < 1) 
		{
			AudioManager.Instance.PlayGameOver ();

			StopGame ();

			gameOver.text = "Game Over";
			gameOver.gameObject.SetActive (true);

			_isGameOver = true;		
		} else 
		{
			StopGame ();
			InitGameObjects ();
		}
	}

	private void FullReset()
	{
		if(OnFullReset != null)
			OnFullReset ();

		Setup ();
	}

	private void StopGame()
	{
		TouchRelease ();
		Destroy (_paddle);
		Destroy (_ball);
	}

	public void LoseLife()
	{
		CurrentLives--;

		CheckGameOver();
	}

	public void BrickDeath()
	{
		_currentNumberOfBricks--;

		CheckWin();
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
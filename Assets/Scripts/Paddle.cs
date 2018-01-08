using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Paddle : MonoBehaviour
{
	public float paddleSpeed = 1f;

	private float _leftRange;
	private float _rightRange;

	private float _yPos;
	private float _direction;

	void OnDestroy()
	{
		GameManager.OnLeftTouch -= MoveLeft;
		GameManager.OnRightTouch -= MoveRight;
		GameManager.OnTouchRelease -= TouchRelease;
	}

	void Start ()
	{
		_yPos = transform.position.y;

		GameManager.OnLeftTouch += MoveLeft;
		GameManager.OnRightTouch += MoveRight;
		GameManager.OnTouchRelease += TouchRelease;

		var renderer = GetComponent<SpriteRenderer> ();

		_leftRange = GameManager.Instance.LeftBorder + (renderer.bounds.size.x / 2);
		_rightRange = GameManager.Instance.RightBorder - (renderer.bounds.size.x/2);
	}

	void MoveLeft () 
	{
		_direction = -1f;
	}

	void MoveRight () 
	{
		_direction = 1f;
	}

	void TouchRelease()
	{
		_direction = 0;
	}

	void FixedUpdate()
	{
		float xPos = transform.position.x + (_direction * paddleSpeed);
		var playerPos = new Vector3 (Mathf.Clamp (xPos, _leftRange, _rightRange), _yPos, 0f);
		transform.position = playerPos;
	}
}
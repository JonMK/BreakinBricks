﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
	public float paddleSpeed = 1f;

	public float movementRange = 8.5f;

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
		var playerPos = new Vector3 (Mathf.Clamp (xPos, -movementRange, movementRange), _yPos, 0f);
		transform.position = playerPos;
	}
}
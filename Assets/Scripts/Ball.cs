using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	public float ballInitialVelocity = 600f;

	private Rigidbody2D _rb;
	private bool _ballInPlay;

	void Awake ()
	{
		_rb = GetComponent<Rigidbody2D>();
		_ballInPlay = false;
	}

	void Update () 
	{
		if ((Input.GetMouseButtonDown (0) || Input.touchCount > 0) && _ballInPlay == false)
		{
			transform.parent = null;
			_ballInPlay = true;
			//_rb.isKinematic = false;
			//_rb.bodyType = RigidbodyType2D.Dynamic;
			//_rb.gravityScale = 0;
			_rb.AddForce (new Vector2 (ballInitialVelocity, ballInitialVelocity));
		}
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{	
		AudioManager.Instance.PlayBallBounce ();
	}
}

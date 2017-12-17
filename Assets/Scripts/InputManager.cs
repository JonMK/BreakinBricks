using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
	private bool _usingTouch;

	void Update () 
	{
		if (Input.GetMouseButtonDown (0) || Input.touchCount > 0)
		{
			Vector3 touchPosWorld;

			if (Input.touchCount > 0) 
			{
				_usingTouch = true;
				touchPosWorld = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
			}
			else
				touchPosWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			Vector2 touchPosWorld2D = new Vector2 (touchPosWorld.x, touchPosWorld.y);

			RaycastHit2D hitInformation = Physics2D.Raycast (touchPosWorld2D, Camera.main.transform.forward);

			if (hitInformation.collider != null) {
				GameObject touchedObject = hitInformation.transform.gameObject;

				switch (touchedObject.tag) {

				case "LeftTouch":
					GameManager.Instance.LeftTouch ();
					break;

				case "RightTouch":
					GameManager.Instance.RightTouch ();
					break;

				}
			}
		} 
		else if (Input.GetMouseButtonUp (0) || (Input.touchCount == 0 && _usingTouch))
		{
			GameManager.Instance.TouchRelease ();
		}
	}
}

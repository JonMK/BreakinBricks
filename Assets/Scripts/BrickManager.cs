using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour 
{
	public int numberOfBricks = 24;
	public GameObject brickPrefab;

	public float horizontalSpacer = .5f;
	public float verticalSpacer = .7f;
	public float topSpacer = 1f;

	private List<GameObject> _brickPool;

	void OnDestroy()
	{
		GameManager.OnFullReset += Reset;
	}
		
	public BrickManager()
	{
		_brickPool = new List<GameObject> ();

		GameManager.OnFullReset += Reset;
	}

	void Awake()
	{
		InitBricks ();
	}

	private void InitBricks()
	{
		var brickTemp =  Instantiate(brickPrefab, transform.position, Quaternion.identity) as GameObject;
		var spriteWidth = brickTemp.GetComponent<SpriteRenderer> ().bounds.size.x;
		var spriteHeight = brickTemp.GetComponent<SpriteRenderer> ().bounds.size.y;
		var worldDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));

		int bricksPerRow = (int)((worldDimensions.x - horizontalSpacer) / (spriteWidth + horizontalSpacer));
		Destroy (brickTemp);

		//Debug.Log (spriteWidth + " : " + worldDimensions + " : " + bricksPerRow);

		horizontalSpacer += ((worldDimensions.x - horizontalSpacer) - ((spriteWidth + horizontalSpacer) * bricksPerRow)) / bricksPerRow;

		int numberOfRows = numberOfBricks / bricksPerRow;
		int vertBrickMultiplier = 0;
		int vertMultiplier = 1;
		int horzBrickMultiplier = 0;
		int horzMultiplier = 1;

		GameObject brick;
		for (int x = 0; x < numberOfRows; x++) 
		{
			for (int i = 0; i < bricksPerRow; i++) 
			{			
			
				brick = Instantiate (brickPrefab, 
					new Vector2 ((horizontalSpacer * horzMultiplier) + (spriteWidth * horzBrickMultiplier) + (spriteWidth / 2), 
						(worldDimensions.y - ((verticalSpacer * vertMultiplier) + (spriteHeight * vertBrickMultiplier))) - topSpacer), 
					Quaternion.identity) as GameObject;

				_brickPool.Add (brick);

				horzBrickMultiplier++;
				horzMultiplier++;
			}

			vertBrickMultiplier++;
			vertMultiplier++;
			horzBrickMultiplier = 0;
			horzMultiplier = 1;
		}
	}

	private void Reset()
	{
		foreach (var brick in _brickPool) 
		{
			brick.gameObject.SetActive (true);
		}
	}
}

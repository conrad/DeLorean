using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour 
{
	public Motor playerMotor;
	public GameObject L;
	public GameObject R; 

	private bool isGameStarted = false;

	void Start () 
	{
		playerMotor.SetEnginePower(500f);	
	}
	
	void Update () 
	{
		if (!isGameStarted) {
			if (Input.touchCount > 0 || Input.anyKeyDown) {
				RemoveInstructions();
				playerMotor.SetEnginePower(2000f);
			}
		}
	}

	private void RemoveInstructions()
	{
		L.SetActive(false);
		R.SetActive(false);
	}
}

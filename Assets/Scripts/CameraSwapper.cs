using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwapper : MonoBehaviour 
{
	public GameObject topCamera;
	public GameObject rearCamera;

	private FlightChecker flightChecker;



	void Start () {
		flightChecker = FlightChecker.Instance;
		flightChecker.SetTarget(transform);

		ActivateTopCamera();
	}



	void FixedUpdate () {
		if (Input.GetButton("Jump")) {
			ActivateRearCamera();
		} else if (flightChecker.IsAirborne()) {
			ActivateRearCamera();
		} else {
			ActivateTopCamera();
		}
	}



	void ActivateTopCamera()
	{
		topCamera.SetActive(true);
		rearCamera.SetActive(false);
	}



	void ActivateRearCamera()
	{
		rearCamera.SetActive(true);
		topCamera.SetActive(false);
	}
}

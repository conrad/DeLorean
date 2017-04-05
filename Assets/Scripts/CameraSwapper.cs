using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwapper : MonoBehaviour 
{
	public GameObject topCamera;
	public GameObject rearCamera;
	public float airBorneHeight;

	// Use this for initialization
	void Start () {
		ActivateTopCamera();
	}
	
	void FixedUpdate () {
		if (Input.GetButton("Jump")) {
			ActivateRearCamera();
		} else if (IsAirborne()) {
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



	bool IsAirborne()
	{
		RaycastHit hit;
		Ray camRay = new Ray(transform.position, Vector3.down);

		if (Physics.Raycast(camRay, out hit, airBorneHeight)) {
			return false;
		}

		return true;
	}
}

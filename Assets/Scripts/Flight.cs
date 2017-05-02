using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour 
{
	public float airBorneHeight;
	public float rotationFactor;

	private Gyroscope gyro;
	private FlightChecker flightChecker;



	void Start()
	{
		flightChecker = FlightChecker.Instance;
		flightChecker.SetTarget(transform);

		gyro = Input.gyro;
		if(!gyro.enabled)
		{
			gyro.enabled = true;
		}
	}



	void Update () 
	{
		flightChecker.Update();
		if (IsMobile() && flightChecker.IsAirborne()) {
			GyroRotate();
		}
	}



	bool IsMobile()
	{
		return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
	}
		


//	bool IsAirborne()
//	{
//		RaycastHit hit;
//		Ray camRay = new Ray(transform.position, Vector3.down);
//
//		if (Physics.Raycast(camRay, out hit, airBorneHeight)) {
//			return false;
//		}
//
//		return true;
//	}
		


	void GyroRotate()
	{
		transform.Rotate(GyroToUnity(Input.gyro.rotationRate, rotationFactor));
	}



	// The Gyroscope is right-handed.  Unity is left handed.
	private static Vector3 GyroToUnity(Vector3 v, float rotationFactor = 1f)
	{
		return new Vector3(-v.x * rotationFactor, -v.y * rotationFactor, v.z * rotationFactor);
	}
}

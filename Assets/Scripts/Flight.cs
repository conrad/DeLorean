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
		if (flightChecker.IsAirborne()) {
			if (IsMobile()) {
				GyroRotate();
			} else {
				KeyRotate();
			}
		}
	}



	bool IsMobile()
	{
		return !(Application.platform == RuntimePlatform.OSXEditor);
//		return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
	}
		


	void GyroRotate()
	{
		transform.Rotate(GyroToUnity(Input.gyro.rotationRate, rotationFactor));
	}



	// The Gyroscope is right-handed.  Unity is left handed.
	private static Vector3 GyroToUnity(Vector3 v, float rotationFactor = 1f)
	{
		return new Vector3(-v.x * rotationFactor, -v.y * rotationFactor, v.z * rotationFactor);
	}


	void KeyRotate()
	{
		if (Input.GetKey(KeyCode.UpArrow)) {
			Debug.Log("hey, I should be tilting up");
			transform.Rotate(Vector3.up);
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			transform.Rotate(Vector3.down);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate(Vector3.right);
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate(Vector3.left);
		} 
	}
}

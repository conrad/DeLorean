using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour 
{
	public float airBorneHeight;

	private Gyroscope gyro;



	void Start()
	{
		gyro = Input.gyro;
		if(!gyro.enabled)
		{
			gyro.enabled = true;
		}
	}



	void Update () 
	{
		if (IsMobile() && IsAirborne()) {
			GyroRotate();
		}
	}



	bool IsMobile()
	{
		return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
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
		


	// The Gyroscope is right-handed.  Unity is left handed.
	void GyroRotate()
	{
		transform.Rotate(GyroToUnity(Input.gyro.rotationRate));
	}


	private static Vector3 GyroToUnity(Vector3 v)
	{
		return new Vector3(-v.x, -v.y, v.z);
	}


//	private static Quaternion GyroToUnity(Quaternion q)
//	{
//		return new Quaternion(q.x, q.y, -q.z, -q.w);
//	}
}

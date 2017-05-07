using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour 
{
	public float airBorneHeight;
	public float rotationFactor;
	public GameObject centerOfMass;
	public WheelCollider[] wheelColliders;
	public GameObject wings;
	public float wingDeployTime = 3f;

	private Gyroscope gyro;
	private FlightChecker flightChecker;
	private Vector3 foldedWings;
	private Vector3 deployedWings;
	private bool areWingsDeployed = false;



	void Start()
	{
		flightChecker = FlightChecker.Instance;
		flightChecker.SetTarget(transform);
		centerOfMass.SetActive(true);

		foldedWings   = new Vector3(0.1f, 0f, 0.7f);
		deployedWings = new Vector3(0.1f, 1.5f, 0.7f);

		gyro = Input.gyro;
		if(!gyro.enabled)
		{
			gyro.enabled = true;
		}
	}



	void Update () 
	{
		flightChecker.UpdateHeight(wheelColliders);

		SetCenterOfMassStatus();
		SetRotationControl();

		ManageWings();
	}



	void SetCenterOfMassStatus()
	{
		if (flightChecker.IsLanded()) {
			centerOfMass.SetActive(true);
		} else {
			centerOfMass.SetActive(false);
		}
	}



	void SetRotationControl()
	{
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
	private static Vector3 GyroToUnity(Vector3 v, float rotationFactor = 10f)
	{
		return new Vector3(-v.x * rotationFactor, -v.y * rotationFactor, v.z * rotationFactor);
	}



	void KeyRotate()
	{
		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.Rotate(Vector3.left * rotationFactor);
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			transform.Rotate(Vector3.right * rotationFactor);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate(Vector3.up * rotationFactor);
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate(Vector3.down * rotationFactor);
		} 
	}



	void ManageWings()
	{
		if (!areWingsDeployed && flightChecker.IsFlying()) {
			StartCoroutine(ScaleWings(foldedWings, deployedWings, wingDeployTime));
			areWingsDeployed = true;
		} else if (areWingsDeployed && !flightChecker.IsFlying()) {
			StartCoroutine(ScaleWings(deployedWings, foldedWings, wingDeployTime));
			areWingsDeployed = false;
		}
	}



	IEnumerator ScaleWings(Vector3 start, Vector3 end, float totalTime)
	{
		for(float t = 0; t < 1; t += Time.deltaTime / totalTime )
		{
			wings.transform.localScale = Vector3.Lerp(start, end, t);
			yield return null;
		}

	}

}

//
//public void DoScale(Vector3 start, Vector3 end, float totalTime) {
//	StartCoroutine(CR_DoScale(start, end, totalTime));
//}
//IEnumerator CR_DoScale(Vector3 start, Vector3 end, float totalTime) {
//	float t = 0;
//	do {
//		gameObject.transform.localscale = Vector3.Lerp(start, end, t / totalTime);
//		yield return null;
//		t += Time.deltaTime;
//	} while (t < totalTime)
//		gameObject.transform.localscale = end;
//	yield break;
//}

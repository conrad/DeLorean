using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour 
{
	public float airBorneHeight;
	public float rotationFactor;
	public float flightSpeed = 40f;
	public GameObject centerOfMass;
	public WheelCollider[] wheelColliders;
	public float wingDeployTime = 3f;
	public GameObject wings;
	public GameObject leftWingTip;
	public GameObject rightWingTip;

	private Gyroscope gyro;
	private FlightChecker flightChecker;

	private Vector3 foldedWings;
	private Vector3 deployedWings;
	private bool areWingsDeployed = false;
	private bool isDeploymentComplete = false;
	private Rigidbody rb;


	void Start()
	{
		flightChecker = FlightChecker.Instance;
		flightChecker.SetTarget(transform);
		centerOfMass.SetActive(true);

		rb = GetComponent<Rigidbody>();

		foldedWings   = new Vector3(0.1f, 0f, 0.7f);
		deployedWings = new Vector3(0.1f, 1.5f, 0.7f);

		SetUpWingTrails();

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
		ManageMovement();
		ManageContrails();
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
		if (flightChecker.IsAirborne() || flightChecker.IsFlying()) {
			if (IsMobile()) {
				GyroRotate();
//				TouchRotate();
			} else {
				KeyRotate();
			}
		}
	}


	bool IsMobile()
	{
		return !(Application.platform == RuntimePlatform.OSXEditor);
	}
		


	void GyroRotate()
	{
		rb.AddTorque(GyroToUnity(Input.gyro.rotationRate, rotationFactor * rotationFactor));
//		transform.Rotate(GyroToUnity(Input.gyro.rotationRate, rotationFactor));
	}



	void TouchRotate()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

			rb.AddTorque(Camera.main.transform.up * -touchDeltaPosition.x);
			rb.AddTorque(Camera.main.transform.right * touchDeltaPosition.y);
		}
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



	void SetUpWingTrails()
	{
		Material leftTrailMat  = leftWingTip.GetComponent<TrailRenderer>().material;
		Material rightTrailMat = rightWingTip.GetComponent<TrailRenderer>().material;

		Color32 col = leftTrailMat.GetColor("_Color");
		col.a = 50;
		leftTrailMat.SetColor("_Color", col);
		rightTrailMat.SetColor("_Color", col);

		leftTrailMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		leftTrailMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		leftTrailMat.SetInt("_ZWrite", 0);
		leftTrailMat.DisableKeyword("_ALPHATEST_ON");
		leftTrailMat.EnableKeyword("_ALPHABLEND_ON");
		leftTrailMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		leftTrailMat.renderQueue = 3000;

		rightTrailMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		rightTrailMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		rightTrailMat.SetInt("_ZWrite", 0);
		rightTrailMat.DisableKeyword("_ALPHATEST_ON");
		rightTrailMat.EnableKeyword("_ALPHABLEND_ON");
		rightTrailMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		rightTrailMat.renderQueue = 3000;

		leftWingTip.SetActive(false);
		rightWingTip.SetActive(false);
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
		isDeploymentComplete = false;

		for(float t = 0; t < 1; t += Time.deltaTime / totalTime )
		{
			wings.transform.localScale = Vector3.Lerp(start, end, t);
			yield return null;
		}

		isDeploymentComplete = true;
	}



	void ManageContrails() {
		if (flightChecker.IsFlying() && isDeploymentComplete) {
			leftWingTip.SetActive(true);
			rightWingTip.SetActive(true);
		} else {
			leftWingTip.SetActive(false);
			rightWingTip.SetActive(false);
		}
	}




	void ManageMovement()
	{
		if (flightChecker.IsFlying()) {
			rb.useGravity = false;
			rb.velocity = transform.forward * flightSpeed;
		} else if (flightChecker.IsSmashing()) { 
			DiveBomb();
		} else if (flightChecker.IsAirborne()) {
			Glide();
		} else {
			rb.useGravity = true;
		}
	}



	void Glide()
	{
		rb.useGravity = false;

		// Gravity
		rb.velocity -= Vector3.up * Time.deltaTime * 8f;

		rb.velocity += transform.forward * Time.deltaTime * 8f;
	}



	void DiveBomb()
	{
//		transform.rotation = Vector3.down;

//		rb.velocity += transform.forward * flightSpeed * Time.deltaTime;
		rb.velocity += Vector3.down * flightSpeed * Time.deltaTime;
	}

}

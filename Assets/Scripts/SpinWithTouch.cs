using UnityEngine;
using System.Collections;

public class SpinWithTouch : MonoBehaviour
{
	public Rigidbody rb;



	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}



	void Update()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

			rb.AddTorque(Camera.main.transform.up * -touchDeltaPosition.x);
			rb.AddTorque(Camera.main.transform.right * touchDeltaPosition.y);
		}
	}
}

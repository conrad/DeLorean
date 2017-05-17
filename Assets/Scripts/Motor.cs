using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Motor : MonoBehaviour 
{
	/**
	 * Make sure wheels are in the order of:
	 * front left
	 * front right
	 * back left 
	 * back right
	 */
	public GameObject[] wheelObject;
	public Transform centerOfMass;
	public float enginePower;
	public float turningFactor;

	Rigidbody rb;
	private Wheel[] wheel;
	private bool isTurningRight;
	private bool isTurningLeft;
	private float reverseDirection;
	private FlightChecker flightChecker;


	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		flightChecker = FlightChecker.Instance;
		wheel = new Wheel[4];
		wheel[0] = wheelObject[0].GetComponent<Wheel>();
		wheel[1] = wheelObject[1].GetComponent<Wheel>();
		wheel[2] = wheelObject[2].GetComponent<Wheel>();
		wheel[3] = wheelObject[3].GetComponent<Wheel>();
	}



	void Start()
	{
		rb.centerOfMass = centerOfMass.localPosition;
	}



	public void SetEnginePower (float newEnginePower)
	{
		enginePower = newEnginePower;
	}



	void FixedUpdate () 	// Reminder: FixedUpdate is better for physics.
	{
		SetIsTurningRight();
		SetIsTurningLeft();
		Drive();
	}



	void SetIsTurningRight()
	{
		foreach (Touch touch in Input.touches) {
			if (touch.position.x > (Screen.width / 2)) {
				isTurningRight = true;
				return;
			}
		}

		isTurningRight = Input.GetKey("right");
	}



	void SetIsTurningLeft()
	{
		foreach (Touch touch in Input.touches) {
			if (touch.position.x < (Screen.width / 2)) {
				isTurningLeft = true;
				return;
			}
		}

		isTurningLeft = Input.GetKey("left");

		// Set reverse turning angle here because this is called after SetIsTurningRight().
		reverseDirection = isTurningRight ? GetTurnPower() : -GetTurnPower();
	}



	void Drive()
	{
		if (!flightChecker.IsDead()) {
			if (IsBackingUp()) {
				MoveBackward();
				Steer();
			} else {
				MoveForward();
				Steer();
			}
		} else {
			Stop();
		}
	}



	bool IsBackingUp()
	{
		return isTurningRight && isTurningLeft;
	}



	void MoveForward() 
	{
		wheel[0].Move(enginePower);
		wheel[1].Move(enginePower);
	}



	void MoveBackward() 
	{
		wheel[0].Move(-enginePower);
		wheel[1].Move(-enginePower);
	}



	void Stop()
	{
		wheelObject[0].GetComponent<WheelCollider>().motorTorque = 0;
		wheelObject[1].GetComponent<WheelCollider>().motorTorque = 0;
		wheelObject[2].GetComponent<WheelCollider>().motorTorque = 0;
		wheelObject[3].GetComponent<WheelCollider>().motorTorque = 0;

	}



	void Steer()
	{
		if (IsBackingUp()) {
			wheel[0].Turn(reverseDirection);
			wheel[1].Turn(reverseDirection);
		} else if (isTurningRight) {
			wheel[0].Turn(GetTurnPower());
			wheel[1].Turn(GetTurnPower());
		} else if (isTurningLeft) {
			wheel[0].Turn(-GetTurnPower());
			wheel[1].Turn(-GetTurnPower());
		} else {
			wheel[0].Turn(0);
			wheel[1].Turn(0);
		}
	}



	float GetTurnPower()
	{
		float speed = Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2) + Mathf.Pow(rb.velocity.z, 2));

//		Debug.Log("speed: " + speed);

		if ((speed / 5f) >= (turningFactor - 5f)) {
			return 5f;
		}

		return turningFactor - (speed / 5f);
	}
}

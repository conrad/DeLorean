using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelRL;
	public WheelCollider wheelRR;

	public float maxTorque = 10000f;

	private bool isTurningRight;
	private bool isTurningLeft;



	void Start () {
		wheelFL.ConfigureVehicleSubsteps(5f, 12, 15);
		wheelFR.ConfigureVehicleSubsteps(5f, 12, 15);
		wheelRR.ConfigureVehicleSubsteps(5f, 12, 15);
		wheelRL.ConfigureVehicleSubsteps(5f, 12, 15);
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
	}



	void Drive()
	{
		if (IsBackingUp()) {
			MoveBackward();
			Steer();
		} else  {
			MoveForward();
			Steer();
		}
	}



	bool IsBackingUp()
	{
		return isTurningRight && isTurningLeft;
	}


	void MoveForward() 
	{
		wheelFR.motorTorque = maxTorque;
		wheelFL.motorTorque = maxTorque;
	}



	void MoveBackward() 
	{
		wheelFR.motorTorque = -maxTorque * 0.5f;
		wheelFL.motorTorque = -maxTorque * 0.5f;
	}



	void Steer()
	{
		Debug.Log("right: " + isTurningRight + " left: " + isTurningLeft);

		if (IsBackingUp()) {
			wheelFR.steerAngle = 10;
			wheelFL.steerAngle = 10;
		} else if (isTurningRight) {
			wheelFR.steerAngle = 10;
			wheelFL.steerAngle = 10;
		} else if (isTurningLeft) {
			wheelFR.steerAngle = -10;
			wheelFL.steerAngle = -10;
		} else {
			wheelFR.steerAngle = 0;
			wheelFL.steerAngle = 0;
		}
	}
}

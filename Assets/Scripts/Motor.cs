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
	public Wheel[] wheel;
	public Transform centerOfMass;
	public float enginePower;
	public float turnPower;

	Rigidbody rbody;
	private bool isTurningRight;
	private bool isTurningLeft;
	private float reverseDirection;



	void Awake()
	{
		rbody = GetComponent<Rigidbody>();
	}



	void Start()
	{
		rbody.centerOfMass = centerOfMass.localPosition;
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
		reverseDirection = isTurningRight ? turnPower : -turnPower;
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
		wheel[0].Move(enginePower);
		wheel[1].Move(enginePower);
	}



	void MoveBackward() 
	{
		wheel[0].Move(-enginePower);
		wheel[1].Move(-enginePower);
	}



	void Steer()
	{
		if (IsBackingUp()) {
			wheel[0].Turn(reverseDirection);
			wheel[1].Turn(reverseDirection);
		} else if (isTurningRight) {
			wheel[0].Turn(turnPower);
			wheel[1].Turn(turnPower);
		} else if (isTurningLeft) {
			wheel[0].Turn(-turnPower);
			wheel[1].Turn(-turnPower);
		} else {
			wheel[0].Turn(0);
			wheel[1].Turn(0);
		}
	}

}

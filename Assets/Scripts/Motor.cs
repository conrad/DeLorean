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
	 * rear left 
	 * rear right
	 */
	public Wheel[] wheel;
	public float enginePower;
	public float turnPower;



	void FixedUpdate()
	{
		float torque = enginePower * Input.GetAxis("Vertical");
		float turnSpeed = turnPower * Input.GetAxis("Horizontal");

		// front wheel drive
		wheel[0].Move(torque);
		wheel[1].Move(torque);

		// front wheel steering 
		wheel[0].Turn(turnSpeed);
		wheel[1].Turn(turnSpeed);

	}
}

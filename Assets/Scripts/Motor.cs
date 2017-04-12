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



	void Awake()
	{
		rbody = GetComponent<Rigidbody>();
	}



	void Start()
	{
		rbody.centerOfMass = centerOfMass.localPosition;
	}



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

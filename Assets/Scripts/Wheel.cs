﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour 
{
	public WheelCollider wc;
//	public GameObject tire;


	void Awake()
	{
		wc = GetComponent<WheelCollider>();
		wc.ConfigureVehicleSubsteps(5f, 12, 15);
	}



	public void Move(float value) 
	{
		wc.motorTorque = value;
	}



	public void Turn(float value)
	{
		wc.steerAngle = value;
	}
}

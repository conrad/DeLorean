using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPassFilter : MonoBehaviour {

	float AccelerometerUpdateInterval = 1.0f / 60.0f;
	float LowPassKernelWidthInSeconds = 1.0f;

	float LowPassFilterFactor;
	Vector3 lowPassValue = Vector3.zero;

	void Start () 
	{
		lowPassValue = Input.acceleration;
		LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; // tweakable
	}

	Vector3 LowPassFilterAccelerometer() 
	{
		lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);

		return lowPassValue;
	}
}

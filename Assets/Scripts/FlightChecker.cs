using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A singleton for determining whether or not something is airborne.
 */ 
public sealed class FlightChecker
{
	public float airBorneHeight = 1;
	public Transform flyerTransform;

	private static FlightChecker instance = null;
	private static readonly object padlock = new object();
	private bool isAirborne;



	FlightChecker() 
	{
	}
		


	public static FlightChecker Instance
	{
		get
		{
			lock (padlock)
			{
				if (instance == null)
				{
					instance = new FlightChecker();
				}
				return instance;
			}
		}
	}



	public void SetTarget(Transform transform)
	{
		flyerTransform = transform;
	}



	/**
	 * Call this in only one place: Flight.
	 */ 
	public void Update() 
	{
		RaycastHit hit;
		Ray camRay = new Ray(flyerTransform.position, Vector3.down);

		if (Physics.Raycast(camRay, out hit, airBorneHeight)) {
			isAirborne = false;
		} else {
			isAirborne = true;
		}
	}



	public bool IsAirborne()
	{
		Debug.Log(isAirborne);
		return isAirborne;
	}
}

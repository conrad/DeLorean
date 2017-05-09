using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A singleton for determining whether or not something is airborne.
 */ 
public sealed class FlightChecker
{
	public float airBorneHeight = 1;
	public float flightHeight   = 10;
	public Transform flyerTransform;

	private static FlightChecker instance = null;
	private static readonly object padlock = new object();
	private float height;
	private float groundHeight = 0.67838f;
	private int timesCheckedOnGround;
	private int checksForLanding = 20;
	private bool isLanded;



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
	public void UpdateHeight(WheelCollider[] wheelColliders) 
	{
		RaycastHit hit;
		Ray camRay = new Ray(flyerTransform.position, Vector3.down);
		Physics.Raycast(camRay, out hit);
		height = hit.distance;

		SetIsLanded(wheelColliders);
	}



	private bool AreAllWheelsTouching(WheelCollider[] wheelColliders)
	{
		WheelHit hit;
		return wheelColliders[0].GetGroundHit(out hit)
			&& wheelColliders[1].GetGroundHit(out hit)
			&& wheelColliders[2].GetGroundHit(out hit)
			&& wheelColliders[3].GetGroundHit(out hit);
	}



	public float GetHeight()
	{
		return height;
	}



	public float GetFlightHeight()
	{
		return flightHeight;
	}


	public float GetGroundHeight()
	{
		return groundHeight;
	}


	public bool IsAirborne()
	{
		return height >= airBorneHeight;
	}



	public bool IsFlying()
	{
		return height >= flightHeight || height <= 0f;
	}



	private bool SetIsLanded(WheelCollider[] wheelColliders)
	{
		if (IsFlying()) {
			timesCheckedOnGround = 0;
			isLanded = false;
			return isLanded;
		}

		if (isLanded) {
			return isLanded;
		}
			
		if (AreAllWheelsTouching(wheelColliders)) {
			if (timesCheckedOnGround >= checksForLanding) {
				timesCheckedOnGround = 0;
				isLanded = true;
			} else {
				timesCheckedOnGround++;
			}
		}

		return isLanded;
	}



	public bool IsLanded()
	{
		return isLanded;
	}
}

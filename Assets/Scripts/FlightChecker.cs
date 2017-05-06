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
	public void UpdateHeight() 
	{
		RaycastHit hit;
		Ray camRay = new Ray(flyerTransform.position, Vector3.down);
		Physics.Raycast(camRay, out hit);
		height = hit.distance;
	}



	public float getHeight()
	{
		return height;
	}



	public float getFlightHeight()
	{
		return flightHeight;
	}


	public float getGroundHeight()
	{
		return groundHeight;
	}


	public bool IsAirborne()
	{
		return height >= airBorneHeight;
	}



	public bool isFlying()
	{
		return height >= flightHeight;
	}
}

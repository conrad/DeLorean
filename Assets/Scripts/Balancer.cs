using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : MonoBehaviour 
{
	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelRL;
	public WheelCollider wheelRR;
	public float correctionRate = 1f;

	bool fl;
	bool rl;
	bool fr;
	bool rr;



	void Update () 
	{
		WheelHit hit;

		fl = wheelFL.GetGroundHit(out hit);
		rl = wheelRL.GetGroundHit(out hit);
		fr = wheelFR.GetGroundHit(out hit);
		rr = wheelRR.GetGroundHit(out hit);


		if (!areAllTouching() && !isJumping()) {
//			transform.Rotate(0f, 0f, Time.deltaTime * correctionRate);
		}
	}
		


	private int getRotateDirection()
	{
		if (!fl && !rl && !fr && !rr) {
			return 0;
		}

		if (!fr && !rr) {
			return -1;		// rotate to the right on z-axis
		}

		if (!fl && !rl) {
			return 1;		// rotate to the left on z-axis
		}

		return 0;
	}



	private bool areAllTouching()
	{
		return fl && rl && fr && rr;
	}


	// TODO: Consider the circumstances where we want to be considered jumping.
	private bool isJumping()
	{
		return areAllTouching();// && transform.rotation.x != 0f;
	}
}

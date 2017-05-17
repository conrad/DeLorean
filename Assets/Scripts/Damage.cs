using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour 
{
	public GameObject smoke;

	FlightChecker flightChecker;
	Collider collider;
	int damage = 0;



	void Start () 
	{
		flightChecker = FlightChecker.Instance;
//		GameObject colliderObject = GameObject.FindGameObjectWithTag("PlayerCollider"); 
//		collider = colliderObject.GetComponent<BoxCollider>();
	}


	void OnCollisionEnter(Collision collision)
	{
		Debug.Log("collision data: " + collision.collider);

		if (!flightChecker.IsLanded()) {
			damage += 1;
			Debug.Log("Flying Collision: " + damage);
			if (damage > 3) {
				smoke.SetActive(true);
			}
		}
	}



//	void Update () 
//	{
//		if (flightChecker.IsFlying()) {
//			
//		}
//			
//	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Damage : MonoBehaviour 
{
	public GameObject smoke;
	public int smokeLevelDamage = 7;
	public int deadDamage = 10;


	FlightChecker flightChecker;
	Collider collider;
	int damage = 0;




	void Start () 
	{
		flightChecker = FlightChecker.Instance;
	}



	void OnCollisionEnter(Collision collision)
	{
		if (!flightChecker.IsLanded()) {
			Debug.Log("damage!!");
			damage += 1;

			if (damage > smokeLevelDamage) {
				smoke.SetActive(true);
			}

			if (damage > deadDamage) {
				flightChecker.SetIsDead(true);
			}
		}
	}
}

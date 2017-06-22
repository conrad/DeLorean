using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Damage : MonoBehaviour 
{
	public GameObject[] damagedSmoke;
	public GameObject[] deadSmoke;
	public int smokeLevelDamage = 5;
	public int deadDamage = 10;
	public MeshRenderer carMesh;


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
//			Debug.Log("collision data: " + collision.rigidbody.name);
			Debug.Log("damage!! " + damage);
			damage += 1;

			if (damage > smokeLevelDamage) {
				for (int i = 0; i < damagedSmoke.Length; i++) {
					damagedSmoke[i].SetActive(true);
				}
			}

			if (damage > deadDamage) {
				flightChecker.SetIsDead(true);
				carMesh.material.color = new Color(0f, 0f, 0f);

				for (int i = 0; i < deadSmoke.Length; i++) {
					deadSmoke[i].SetActive(true);
				}
			}
		}
	}
}

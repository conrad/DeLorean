using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour 
{
	public GameObject target;

	private float offset;

	void Start()
	{
		offset = Vector3.Distance(transform.position, target.transform.position);
	}

	void Update () {
		transform.position = (transform.position - target.transform.position).normalized * offset + target.transform.position;
		transform.LookAt(target.transform);
	}
}

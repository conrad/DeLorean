using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidMarks : MonoBehaviour 
{
	public WheelCollider correspondingCollider;
	public GameObject skidMarkObject;



	void Start () {
		skidMarkObject.SetActive(true);
	}



	void Update () {
		RaycastHit hit;

		// Find the collider's center point, you need to do this because the center of the collider might not actually be
		// the real position if the transform's off.
//		Vector3 colliderCenterPoint = correspondingCollider.transform.TransformPoint(correspondingCollider.center);

//		-correspondingCollider.transform.up + correspondingCollider.suspensionDistance + correspondingCollider.radius

		Ray skidRay = new Ray(skidMarkObject.transform.position, -skidMarkObject.transform.up);


		if (Physics.Raycast(skidRay, out hit, 0.5f)) {
			skidMarkObject.SetActive(true);
		} else {
			skidMarkObject.SetActive(false);
		}


//		if (Physics.Raycast(colliderCenterPoint, -correspondingCollider.transform.up, out hit, correspondingCollider.suspensionDistance + correspondingCollider.radius) ) {
//			transform.position = hit.point + (correspondingCollider.transform.up * correspondingCollider.radius);
//			Debug.Log("Touching");
//		} else {
//			transform.position = colliderCenterPoint - (correspondingCollider.transform.up * correspondingCollider.suspensionDistance);
//			Debug.Log("NOT");
//		}

	}
}

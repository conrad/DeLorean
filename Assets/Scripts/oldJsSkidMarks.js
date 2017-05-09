//#pragma strict
//
//var CorrespondingCollider : WheelCollider;
//var skidMarkPrefab : GameObject;
//
//function Start() {
//	skidMarkPrefab.gameObject.active = false;
//}
//
//function Update () {
//
//	// define a hit point for the raycast collision
//	var hit : RaycastHit;
//	// Find the collider's center point, you need to do this because the center of the collider might not actually be
//	// the real position if the transform's off.
//	var ColliderCenterPoint : Vector3 = CorrespondingCollider.transform.TransformPoint( CorrespondingCollider.center );
//	
//	// now cast a ray out from the wheel collider's center the distance of the suspension, if it hit something, then use the "hit"
//	// variable's data to find where the wheel hit, if it didn't, then se tthe wheel to be fully extended along the suspension.
//	if ( Physics.Raycast( ColliderCenterPoint, -CorrespondingCollider.transform.up, hit, CorrespondingCollider.suspensionDistance + CorrespondingCollider.radius ) ) {
//		transform.position = hit.point + (CorrespondingCollider.transform.up * CorrespondingCollider.radius);
//	}else{
//		transform.position = ColliderCenterPoint - (CorrespondingCollider.transform.up * CorrespondingCollider.suspensionDistance);
//	}
//	
//	// define a wheelhit object, this stores all of the data from the wheel collider and will allow us to determine
//	// the slip of the tire.
//	var CorrespondingGroundHit : WheelHit;
//	CorrespondingCollider.GetGroundHit( CorrespondingGroundHit );
//	
//	// if the slip of the tire is greater than 2.0, and the slip prefab exists, create an instance of it on the ground at
//	// a zero rotation.
//	if ( Mathf.Abs( CorrespondingGroundHit.sidewaysSlip ) > .8 )
//		{
//		skidMarkPrefab.gameObject.active = true;
//		}
//	else if ( Mathf.Abs( CorrespondingGroundHit.sidewaysSlip ) <= .75 )
//		{
//		skidMarkPrefab.gameObject.active = false;
//		}
//	}
//

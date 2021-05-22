using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFollowerController : RaycastController
{
	public Transform targetObject;
    
	public LayerMask passengerMask;
	public Vector3 lastPosition;

	public override void Start () {
		base.Start ();
		lastPosition = transform.position;
		transform.position = targetObject.position;
	}
	void Update ()
	{
		UpdateRaycastOrigins ();
		transform.position = targetObject.position;
		Vector3 velocity = (transform.position - lastPosition);
		lastPosition = transform.position;
		MovePassengers(velocity);
	        
        
	}

	void MovePassengers(Vector3 velocity) {
		HashSet<Transform> movedPassengers = new HashSet<Transform> ();

		float directionX = Mathf.Sign (velocity.x);
		float directionY = Mathf.Sign (velocity.y);

		// Vertically moving platform
		if (velocity.y != 0) {
			float rayLength = Mathf.Abs (velocity.y) + skinWidth;
			
			for (int i = 0; i < verticalRayCount; i ++) {
				Vector2 rayOrigin = (directionY == -1) ? origins.botLeft : origins.topLeft;
				rayOrigin += Vector2.right * (verticalRaySpacing * i);
                
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

				if (hit) {
                    Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.green);
					if (!movedPassengers.Contains(hit.transform)) {
						movedPassengers.Add(hit.transform);
						float pushX = (directionY == 1) ? velocity.x : 0;
						float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

						hit.transform.Translate(new Vector3(pushX,pushY));
					}
				}
                else {
                    Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);
                }
			}
		}

		// Horizontally moving platform
		if (velocity.x != 0) {
			float rayLength = Mathf.Abs (velocity.x) + skinWidth;
			
			for (int i = 0; i < horizontalRayCount; i ++) {
				Vector2 rayOrigin = (directionX == -1) ? origins.botLeft : origins.botRight;
				rayOrigin += Vector2.up * (horizontalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

				if (hit) {
					if (!movedPassengers.Contains(hit.transform)) {
						movedPassengers.Add(hit.transform);
						float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
						float pushY = 0;
						
						hit.transform.Translate(new Vector3(pushX,pushY));
					}
				}
			}
		}

		// Passenger on top of a horizontally or downward moving platform
		if (directionY == -1 || velocity.y == 0 && velocity.x != 0) {
			float rayLength = skinWidth * 2;
			
			for (int i = 0; i < verticalRayCount; i ++) {
				Vector2 rayOrigin = origins.topLeft + Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);
				
				if (hit) {
					if (!movedPassengers.Contains(hit.transform)) {
						movedPassengers.Add(hit.transform);
						float pushX = velocity.x;
						float pushY = velocity.y;
						
						hit.transform.Translate(new Vector3(pushX,pushY));
					}
				}
			}
		}
	}
}

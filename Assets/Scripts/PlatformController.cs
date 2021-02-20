using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : RaycastController
{

	public float firstPosY;
	public float secondPosY;

	public LayerMask passengerMask;
	public Vector3 move;
	BoxCollider2D col;
	public override void Start () {
		base.Start ();
        col = GetComponentsInChildren<BoxCollider2D>()[1];
	}
	void Update () {
		UpdateRaycastOrigins ();

		Vector3 velocity = move * Time.deltaTime;
        
        if(col.IsTouchingLayers(passengerMask)) {	
            transform.Translate (velocity);
            MovePassengers(velocity);
        }
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

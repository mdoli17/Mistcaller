using System.Collections;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class RaycastController : MonoBehaviour {

	public LayerMask collisionMask;
	
	public const float skinWidth = .015f;
	[Range(0,15)]
	public int horizontalRayCount = 4;
	[Range(0,15)]
	public int verticalRayCount = 4;

	[HideInInspector]
	public float horizontalRaySpacing;
	[HideInInspector]
	public float verticalRaySpacing;

	[HideInInspector]
	public BoxCollider2D collider;
	public RaycastOrigins origins;

	public virtual void Start() {
		collider = GetComponent<BoxCollider2D> ();
		CalculateRaySpacing ();
	}

	public void UpdateRaycastOrigins() {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);
		
		origins.botLeft = new Vector2 (bounds.min.x, bounds.min.y);
		origins.botRight = new Vector2 (bounds.max.x, bounds.min.y);
		origins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		origins.topRight = new Vector2 (bounds.max.x, bounds.max.y);

	}
	
	public void CalculateRaySpacing() {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);
		
		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);
		
		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}


	public struct RaycastOrigins {
		public Vector2 topLeft, topRight;
		public Vector2 botLeft, botRight;
	}

	private void OnDrawGizmos() {
		
	}
}
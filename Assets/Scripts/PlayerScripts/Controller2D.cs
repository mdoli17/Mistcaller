using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : RaycastController
{

    public float maxSlopeAngle = 80;

    public CollisionInfo collisions;
    void Start()
    {
        base.Start();
        collider = GetComponent<BoxCollider2D>();   
    }

    void horizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for(int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? origins.botLeft : origins.botRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, directionX * Vector2.right, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);
            if(hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                if(i == 0 && slopeAngle <= maxSlopeAngle)
                {
                    if(collisions.descendingSlope)
                    {
                        collisions.descendingSlope = false;
                        velocity = collisions.velocityOld;
                    }
                    float distanceToSlopeStart = 0;
                    if (slopeAngle  != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        velocity.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref velocity, slopeAngle, hit.normal);
                    velocity.x += distanceToSlopeStart * directionX;
                }
                if(!collisions.climbingSlope || slopeAngle > maxSlopeAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    if(collisions.climbingSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
            }
        }
    }

    private void ClimbSlope(ref Vector3 velocity, float slopeAngle, Vector2 slopeNormal)
    {
        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
        if (velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
            collisions.slopeNormal = slopeNormal;
        }
        
    }

    private void DescendSlope(ref Vector3 velocity)
    {
        RaycastHit2D maxSlopeHitLeft = Physics2D.Raycast(origins.botLeft, Vector2.down, Mathf.Abs(velocity.y) + skinWidth, collisionMask);
        RaycastHit2D maxSlopeHitRight = Physics2D.Raycast(origins.botRight, Vector2.down, Mathf.Abs(velocity.y) + skinWidth, collisionMask);
        if (maxSlopeHitLeft ^ maxSlopeHitRight) {
            slideDownMaxSlope(maxSlopeHitLeft, ref velocity);
            slideDownMaxSlope(maxSlopeHitRight, ref velocity);
        }

        if(!collisions.slidingDownMaxSlope) {
            float directionX = Mathf.Sign(velocity.x);
            Vector2 rayOrigin = (directionX == -1) ? origins.botRight : origins.botLeft;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);
            
            if(hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if(slopeAngle != 0 && slopeAngle <= maxSlopeAngle)
                {
                    if(Mathf.Sign(hit.normal.x) == directionX)
                    {
                        if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
                        {
                            float moveDistance = Mathf.Abs(velocity.x);
                            float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                            velocity.y -= descendVelocityY;

                            collisions.slopeAngle = slopeAngle;
                            collisions.descendingSlope = true;
                            collisions.below = true;
                            collisions.slopeNormal = hit.normal;
                        }
                    }
                }
            }
        }
    }

    void slideDownMaxSlope(RaycastHit2D hit2D, ref Vector3 moveAmmount)
    {
        if(hit2D)
        {
            float slopeAngle = Vector2.Angle(hit2D.normal, Vector2.up);
            if(slopeAngle > maxSlopeAngle)
            {
                moveAmmount.x = Mathf.Sign(hit2D.normal.x) * (Mathf.Abs(moveAmmount.y) - hit2D.distance) / Mathf.Tan(slopeAngle * Mathf.Deg2Rad);
            
                collisions.slopeAngle = slopeAngle;
                collisions.slidingDownMaxSlope = true;
                collisions.slopeNormal = hit2D.normal;
            }
        }
    }

    void verticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? origins.botLeft : origins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, directionY * Vector2.up, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
            if(hit)
            {
                
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                if(collisions.climbingSlope)
                {
                    velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x); 
                }

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
                
            }
        }
        if(collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? origins.botLeft : origins.botRight) + Vector2.up * velocity.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            if(hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if(slopeAngle != collisions.slopeAngle)
                {
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }
    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        collisions.velocityOld = velocity;

        if(velocity.y < 0)
        {
            DescendSlope(ref velocity);
        }
        if(velocity.x != 0)
        {
            horizontalCollisions(ref velocity);
        }
        if(velocity.y != 0)
        {
            verticalCollisions(ref velocity);
        }
        transform.Translate(velocity);
    }

    public struct CollisionInfo
    {
        public bool above, below, left, right;
        public bool climbingSlope, descendingSlope;
        
        public bool slidingDownMaxSlope;
        public float slopeAngle, slopeAngleOld;

        public Vector2 slopeNormal;
        public Vector3 velocityOld;
        public void Reset()
        {
            slidingDownMaxSlope = above = below = left = right = climbingSlope =  false;
            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
            slopeNormal = Vector2.zero;
        }
    }

}

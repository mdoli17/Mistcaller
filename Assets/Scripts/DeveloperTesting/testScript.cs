using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : RaycastController
{
    [Range(0,1)]
    public float MaxRotation;
    Vector3 velocity;   
    public float speed;

    SwingController controller;
    private void Start() {
        base.Start();
        controller = GetComponentInChildren<SwingController>();
        velocity = Vector3.zero;
    }
    private void Update() {
        UpdateRaycastOrigins();
        if(transform.rotation.z > MaxRotation || transform.rotation.z < -MaxRotation)
        {
            velocity.z = 0;
        }

        float force = controller.getRotationForce();
        if(transform.rotation.z < MaxRotation && transform.rotation.z > -MaxRotation)
        {
            velocity.z += force;
        }
        else if(transform.rotation.z > MaxRotation)
        {
            if(force < 0)
            {
                velocity.z += force;
            }
        }
        else if(transform.rotation.z < -MaxRotation)
        {
            if(force > 0)
            {
                velocity.z += force;
            }
        }
        transform.Rotate(velocity * Time.deltaTime * speed);

        MovePassengers();
    }

    private void MovePassengers() {
        for(int i = 0; i < 15; i++)
        {
            Vector3 startposition = transform.position - new Vector3(collider.size.x * 5,0,0);
            Debug.DrawRay(startposition, Vector3.up, Color.blue);

            
        }
    }
}

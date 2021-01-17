using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    [Range(0,1)]
    public float MaxRotation;
    Vector3 velocity;   
    public float speed;

    SwingController controller;
    private void Start() {
        controller = GetComponentInChildren<SwingController>();
        
        velocity = Vector3.zero;
    }
    private void Update() {
        Debug.Log(controller.gameObject.name);
        
        if(transform.rotation.z > MaxRotation || transform.rotation.z < -MaxRotation)
        {
            Debug.Log("Reseting Z");
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class MovableScript : MonoBehaviour
{

    Controller2D controller;
    Vector3 velocity;


    float velocityXSmoothing;
    float targetVelocityX;

    public float gravity;
    // Start is called before the first frame update
    void Start()
    {
        
        controller = GetComponent<Controller2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void setTargetVelocityX(float value)
    {
        velocity.x = value;
    }
}

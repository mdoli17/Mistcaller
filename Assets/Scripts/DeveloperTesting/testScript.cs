using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    [Range(0,1)]
    public float MaxRotation;
    public Player player;
    Vector3 velocity;   
    public LayerMask mask;
    BoxCollider2D collider;
    private void Start() {
        collider = GetComponentsInChildren<BoxCollider2D>()[1];
    }
    private void Update() {
        if(transform.rotation.z > MaxRotation || transform.rotation.z < -MaxRotation)
        {
            velocity.z = 0;
        }
        if(collider.IsTouchingLayers(mask))
        {   
            if(transform.rotation.z < MaxRotation && transform.rotation.z > -MaxRotation)
            {
                velocity.z += (transform.position.x - player.transform.position.x);
            }
            else if(transform.rotation.z > MaxRotation)
            {
                if(transform.position.x - player.transform.position.x < 0)
                {
                    velocity.z += (transform.position.x - player.transform.position.x);
                }
            }
            else if(transform.rotation.z < -MaxRotation)
            {
                if(transform.position.x - player.transform.position.x > 0)
                {
                    velocity.z += (transform.position.x - player.transform.position.x);
                }
            }
        }
        transform.Rotate(velocity * Time.deltaTime * 0.1f);
    }
}

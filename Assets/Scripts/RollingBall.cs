using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{
    public GameObject rollingObject;
    public Vector3 targetLocation;
    private Vector3 velocity = Vector3.zero;
    public float ballSpeed = 1;
    private float velocityXSmoothing;
    public float accelerationTimeGrounded = 0.5f;
    public float rotationSpeed;
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(velocity);
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            Player playerController = other.gameObject.GetComponent<Player>();
            float direction = Mathf.Sign(playerController.getVelocity().x);
            if(playerController.getVelocity().x != 0)
            {
                float targetVelocityX = playerController.getVelocity().x * ballSpeed;
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (other.GetComponent<Controller2D>().collisions.below) ? accelerationTimeGrounded : 5);
                
            }
        }
        
    }

    void Update()
    {
        if (transform.position.x < targetLocation.x)
        {
            transform.parent.transform.Translate(velocity);
            rollingObject.transform.Rotate(new Vector3(0, 0, -1f * velocity.x * rotationSpeed));
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(targetLocation, 0.5f);
    }
   
}

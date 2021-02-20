using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class SwingController : MonoBehaviour
{
    BoxCollider2D collider2D;

    List<GameObject> applicableObjects;
    public LayerMask applicableObjectMask;
    private void Start() {
        applicableObjects = new List<GameObject>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Movable"))
        {
            Debug.Log(other.gameObject.name);
            applicableObjects.Add(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Movable"))
        {
            applicableObjects.Remove(other.gameObject);
        }
    }

    public float getRotationForce()
    {
        float velocity = 0;

        foreach (var item in applicableObjects)
        {
            Debug.Log(item.name);
            PhysicsAttributes body = item.GetComponentInChildren<PhysicsAttributes>();
            float length = (transform.position - item.transform.position).x;
            float zForce = body.Mass * length;

            velocity += zForce;
        }
        return velocity;
    }
}

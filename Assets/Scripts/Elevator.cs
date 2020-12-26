using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Elevator : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            transform.Translate(0,0.1f,1);
        }
    }
}

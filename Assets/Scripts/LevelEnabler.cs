using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (BoxCollider2D))]
public class LevelEnabler : MonoBehaviour
{
    public GameObject obj;

    private void Start() {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(collider.size.x, 60);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            obj.SetActive(true);
        }
    }

}

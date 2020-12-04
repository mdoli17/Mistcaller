using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class InteractChecker : MonoBehaviour
{
    private BoxCollider2D checker;
    public LayerMask masks;

    private GameObject interactableObject;
    void Start()
    {
        checker = GetComponent<BoxCollider2D>();
        checker.isTrigger = true;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    { 
        if (masks == (masks | (1 << other.gameObject.layer)))
        {
            interactableObject = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (masks == (masks | (1 << other.gameObject.layer)))
        {
            interactableObject = null;
        }
    }

    public GameObject getInteractableObject()
    {
        return interactableObject;
    }
}


interface Interactable
{
    void Interact();
}

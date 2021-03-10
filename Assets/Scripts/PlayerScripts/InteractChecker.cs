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
    


    public GameObject getInteractableObject()
    {
        if(checker.IsTouchingLayers(masks))
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(masks);
            Collider2D[] results = new Collider2D[1];
            checker.OverlapCollider(filter, results);
            Debug.Log(results[0] == null);
            return results[0].gameObject;
        }
        return null;
    }
}


public interface Interactable
{
    void Interact();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class HandleCheck : MonoBehaviour
{

    bool bIsInteractingWithHandle;
    GameObject ControlledObject;
    bool bIsGrabed;

    BoxCollider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        collider2D.isTrigger = true;
        bIsInteractingWithHandle = false;
        bIsGrabed = false;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Handle"))
        {
            bIsInteractingWithHandle = true;
            ControlledObject = other.gameObject;

        }    
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Handle"))
        {
            bIsInteractingWithHandle = false;
            ControlledObject = null;

        }    
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Handle"))
        {
            if(bIsGrabed)
            {
                MovableScript movable = other.gameObject.GetComponentInParent<MovableScript>();
                
                movable.setTargetVelocityX(GetComponentInParent<NewPlayer>().getVelocity().x);
            }
        }
    }

    public bool CanReachHandle()
    {
        return bIsInteractingWithHandle;
    }

    public void PushObject(float force)
    {
        
    }

    public void grab()
    {
        bIsGrabed = true;
    }

    public void letGo()
    {
        bIsGrabed = false;

        if(ControlledObject)
        {
            MovableScript movable = ControlledObject.GetComponentInParent<MovableScript>();
            if(movable)
            {
                movable.setTargetVelocityX(0);
            }
        }
    }
}

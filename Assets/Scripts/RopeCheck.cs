using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCheck : MonoBehaviour
{
    
    private List<Collider2D> colliders;

    // Start is called before the first frame update
    void Start()
    {
        colliders = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Rope"))
        {
            
            GetComponentInParent<Controller>().RopeDetected();
            colliders.Add(other);
        }    
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Rope"))
        {
            // GetComponentInParent<Controller>().RopeLeft();
            colliders.Remove(other);
        }    
    }

    public Collider2D[] getRopes()
    {
        return colliders.ToArray();
    }
}

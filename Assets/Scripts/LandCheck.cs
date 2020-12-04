using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandCheck : MonoBehaviour
{
    Controller MistyController;

    // Start is called before the first frame update
    void Start()
    {
        MistyController = GetComponentInParent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        // Vector2 impact = other.GetContact(0).point;
        
        // Vector2 impact2 = other.GetContact(1).point;
        // Debug.DrawRay(transform.position, impact - new Vector2(transform.position.x, transform.position.y), Color.blue, 0f);
        // Debug.DrawRay(transform.position, impact2 - new Vector2(transform.position.x, transform.position.y), Color.blue, 0f);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        MistyController.HasLanded();
    }

    void OnCollisionExit2D(Collision2D other) 
    {
        MistyController.NotOnGround();    
    }
}

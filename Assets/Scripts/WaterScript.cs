using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{

    public float upthrust;

    BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.transform.gameObject.GetComponent<Controller>().inWater();
        }    
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.transform.gameObject.GetComponent<Controller>().outOfWater();
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("movable")) return;

        Rigidbody2D rb = other.transform.gameObject.GetComponent<Rigidbody2D>();
        if(rb == null) return;

        float boundY = transform.position.y + col.bounds.size.y / 2;
        float halfSize = other.bounds.size.y / 2;

        float x = transform.position.y - boundY;
        float deltaH = halfSize - x;

        rb.AddForce(Vector2.up * upthrust * deltaH * other.bounds.size.x);
    
    }



}

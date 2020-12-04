using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPad : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.transform.gameObject.GetComponentInChildren<HealthComponent>().TakeDamage(150);
        }
    }

}

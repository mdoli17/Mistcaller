using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
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
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            HealthComponent health = other.gameObject.GetComponent<HealthComponent>();
            
            StartCoroutine(DamageDealer(health));
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StopAllCoroutines();
        }
    }

    IEnumerator DamageDealer(HealthComponent health)
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            if(health != null)
            {
                health.TakeDamage(10);
            }
        }
    }

}

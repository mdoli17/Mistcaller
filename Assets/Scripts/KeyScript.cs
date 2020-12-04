using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject widget;
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
            Controller ct = other.gameObject.GetComponent<Controller>();
            if(ct)
            {
                widget.SetActive(true);
                ct.keyPicked = true;
                Destroy(gameObject);
            }
        }
    }
}

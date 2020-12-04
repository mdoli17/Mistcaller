using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
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


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(other.gameObject.GetComponent<Controller>().keyPicked)
            {
                widget.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }
}

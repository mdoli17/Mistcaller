using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistCheck : MonoBehaviour
{

    public List<Collider2D> MistList;

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
        if(other.gameObject.layer == LayerMask.NameToLayer("Mist"))
        {
            MistList.Add(other);

        }
    }
    
    
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Mist"))
        {
            if(MistList.Contains(other))
                MistList.Remove(other);
        }    
    }
}

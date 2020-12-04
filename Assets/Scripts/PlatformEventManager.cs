using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEventManager : MonoBehaviour
{
    public delegate void PlatformPressAction();

    public static event PlatformPressAction OnPlatformPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.isTrigger) return;
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) return;
        OnPlatformPressed();    
    }
}

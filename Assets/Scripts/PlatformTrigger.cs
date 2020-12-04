using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class PlatformTrigger : MonoBehaviour
{
    public LayerMask triggerMask;

    public delegate void TriggerDelegate();
    public TriggerDelegate OnTriggerPressed;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogError("SESDONFUISODUFOSNFUS");
        if(triggerMask == (triggerMask | (1 << other.gameObject.layer)))
        {
            if(OnTriggerPressed != null)
            {
                OnTriggerPressed();
            }
        }
    }
}

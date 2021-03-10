using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class PlatformTrigger : MonoBehaviour
{

    public delegate void TriggerDelegate();
    public TriggerDelegate OnPlayerPressed;
    
    public TriggerDelegate OnObstaclePressed;

    bool obstaclePressed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
    
        if(!obstaclePressed)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                OnPlayerPressed();
            } 
            else if(other.gameObject.layer == LayerMask.NameToLayer("Movable"))
            {
                obstaclePressed = true;
                OnObstaclePressed();
            }
                
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Movable"))
        {
            obstaclePressed = false;
            OnPlayerPressed();
        }
    }
}

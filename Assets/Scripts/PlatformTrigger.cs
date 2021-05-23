using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class PlatformTrigger : MonoBehaviour
{
    public delegate void TriggerDelegate();
    public TriggerDelegate onPlayerPressed;
    public TriggerDelegate onObstaclePressed;

    private bool obstaclePressed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (obstaclePressed) return;
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            onPlayerPressed();
        else if (other.gameObject.layer == LayerMask.NameToLayer("Movable"))
        {
            obstaclePressed = true;
            onObstaclePressed();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Movable")) return;
        obstaclePressed = false;
        onPlayerPressed();
    }
}

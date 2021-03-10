using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Lever lever;
    public PlatformTrigger trigger;

    [SerializeField]
    private float openDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        if (lever) 
        {
            lever.LeverInteractDelegate += leverFunction;  
        }
        if(trigger)
        {
            trigger.OnObstaclePressed += OnObstacleSteppedOnTrigger;
            trigger.OnPlayerPressed += OnPlayerSteppedOnTrigger;
        }
    }

    void OnPlayerSteppedOnTrigger() {
        GetComponent<BoxCollider2D>().enabled = false;
        StopAllCoroutines();
        StartCoroutine(OpenDoorWithTime());
    }
    void OnObstacleSteppedOnTrigger() {
        StopAllCoroutines();
        GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator OpenDoorWithTime() 
    {
        yield return new WaitForSeconds(openDuration);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void leverFunction()
    {
        Destroy(this.gameObject);
    }
}

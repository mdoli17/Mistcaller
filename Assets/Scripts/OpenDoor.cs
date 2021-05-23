using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Lever lever;
    public PlatformTrigger trigger;
    public GameObject door;

    [SerializeField]
    private float openDuration;
    
    // Start is called before the first frame update
    void Start()
    {
        if (lever) 
        {
            lever.LeverInteractDelegate += leverFunction;  
        }

        if (!trigger) return;
        trigger.onObstaclePressed += OnObstacleSteppedOnTrigger;
        trigger.onPlayerPressed += OnPlayerSteppedOnTrigger;
    }

    void OnPlayerSteppedOnTrigger() {
        GetComponent<BoxCollider2D>().enabled = false;
        door.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(OpenDoorWithTime());
    }
    void OnObstacleSteppedOnTrigger() {
        StopAllCoroutines();
        GetComponent<BoxCollider2D>().enabled = false;
        door.SetActive(false);
    }

    IEnumerator OpenDoorWithTime() 
    {
        yield return new WaitForSeconds(openDuration);
        GetComponent<BoxCollider2D>().enabled = true;
        door.SetActive(true);
    }

    void leverFunction()
    {
        Destroy(this.gameObject);
    }
}

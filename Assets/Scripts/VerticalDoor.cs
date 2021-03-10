using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDoor : MonoBehaviour
{
    public PlatformTrigger trigger;
    public float moveTime = 1f;
    public Vector3 targetLocation;

    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
        trigger.OnPlayerPressed += StartMovement;
    }

    void StartMovement()
    {
        Debug.Log("Trigger Pressed");
        StartCoroutine(MoveUp());
    }

    IEnumerator MoveUp()
    {
        float t = 0f;
        while (t < moveTime)
        {
            t += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, startPosition + targetLocation, t / moveTime);
            yield return null;
        }
    }
}

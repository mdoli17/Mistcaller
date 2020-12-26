using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    public Transform TargetTransform;
    public float ResetDelay;
    public float TimeToMove;

    bool canMove;
    Vector3 StartPosition;

    void Start()
    {
        StartPosition = transform.position;
        canMove = true;
        StartCoroutine(MoveToPosition());
    } 

    IEnumerator MoveToPosition()
    {
        
        float t = 0f;
        while(t < 1)
        {
            t += Time.deltaTime / TimeToMove;
            transform.position = Vector2.Lerp(StartPosition, TargetTransform.position, t);
            yield return null;
        }

        StartCoroutine(ResetPosition());
    }


    IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(ResetDelay);
        
        float t = 0f;
        while(t < 1)
        {
            t += Time.deltaTime / TimeToMove;
            transform.position = Vector2.Lerp(TargetTransform.position, StartPosition, t);
            yield return null;
        }

        StartCoroutine(MoveToPosition());
    }
}

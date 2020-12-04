using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    // public PlatformEventManager manager;
    public Transform TargetTransform;
    // Start is called before the first frame update
    
    public float ResetDelay;

    public float TimeToMove;

    bool canMove;
    Vector3 StartPosition;

    void Start()
    {
        StartPosition = transform.position;
        canMove = true;
        // Note: We need to be able to specify which manager to subscribe
        PlatformEventManager.OnPlatformPressed += TriggerAction;

        

    } 
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void TriggerAction()
    {
        if(!canMove)  return;
        canMove = false;
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

        canMove = true;
    }
}

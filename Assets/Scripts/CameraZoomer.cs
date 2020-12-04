using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    public Camera cam;
    public float zoomTime;
    public Vector3 targetLocation;

    public float TargetZoom;
    private float EntryZoom;

    public bool moveCamera;

    Vector3 EntryPosition;
    
    bool inCollision;

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(transform.position.x - transform.lossyScale.x / 2,transform.position.y,0), .1f);
        Gizmos.DrawSphere(transform.position + targetLocation, .5f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EntryZoom = cam.orthographicSize;
            StopAllCoroutines();
            StartCoroutine(zoomOut());
            if(moveCamera)
            {
                cam.GetComponentInParent<CameraScript>().enabled = false;
                StartCoroutine(MoveIn());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(zoomIn());
            cam.GetComponentInParent<CameraScript>().enabled = true;
        }
    }




    IEnumerator zoomOut()
    {
        float t = 0f;
        while (t < zoomTime)
        {
            t += Time.deltaTime;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, TargetZoom, t / zoomTime);
            yield return null;
        }
    }

    IEnumerator zoomIn()
    {
        float t = 0f;
        while (t < zoomTime)
        {
            t += Time.deltaTime;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, EntryZoom, t / zoomTime);
            yield return null;
        }
    }

    IEnumerator MoveIn()
    {
        float t = 0f;
        while (t < zoomTime)
        {
            Debug.LogError(Time.realtimeSinceStartup);
            t += Time.deltaTime;

            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(transform.position.x, transform.position.y, cam.transform.position.z) + targetLocation, t / zoomTime);
            yield return null;
        }
        
    }


}

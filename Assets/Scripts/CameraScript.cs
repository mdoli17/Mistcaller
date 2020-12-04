using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Target;

    public Vector3 Offset;

    public bool notInterfered;
    
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        notInterfered = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(Target.transform.position + Offset, .3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (true)
        {
            Vector3 TargetPosition = new Vector3(Target.transform.position.x,Target.transform.position.y, transform.position.z) + Offset;
            Vector3 SmoothMove = Vector3.Lerp(transform.position, TargetPosition, Speed * Time.deltaTime);
            transform.position = SmoothMove;
        }
    }
}

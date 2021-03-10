using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotation : MonoBehaviour
{
    public GameObject obj;
    float offset;
    public float maxRotation;
    // Start is called before the first frame update
    void Start()
    {
        offset = Mathf.Abs((transform.position - obj.transform.position).x);
    }

    // Update is called once per frame
    void Update()
    {   
        float deltaX = (-offset + Mathf.Abs(transform.position.x - obj.transform.position.x));
        deltaX *= 12;
        if(deltaX < maxRotation && deltaX > 0)
        {
            Vector3 deltaR = new Vector3(0, 0, -deltaX);
            transform.rotation = Quaternion.Euler(deltaR);
        }
    }
}

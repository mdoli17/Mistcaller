using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEventTrigger : MonoBehaviour
{

    public GameObject TriggerObject;

    public GameObject RotatingObject;

    public float rotateAmmount;

    public Camera cam;

    public Transform around;

    public AudioClip HitSound;

    public GameObject objectToDestroy;

    public GameObject HindgeObject;

    public GameObject LightToDestroy;

    
    bool hasHit = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.isTrigger) return;
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) return;

        
        if(other.gameObject.layer == LayerMask.NameToLayer("Movable"))
        {
            if(hasHit) return;
            cam.GetComponent<CameraShake>().Shake(0.25f);
            Rigidbody2D cmp = TriggerObject.AddComponent<Rigidbody2D>();
            cmp.mass = 100f;

            Destroy(objectToDestroy);
            Destroy(LightToDestroy);
            makeHindge(cmp);
            
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.PlayOneShot(HitSound);
            InvokeRepeating("RotateObject",0,0.01f);
            Invoke("StopRotation", 0.4f);
            hasHit = true;
        }

    }

    void makeHindge(Rigidbody2D body)
    {
        HingeJoint2D joint = HindgeObject.AddComponent<HingeJoint2D>();
        joint.connectedBody = body;
    }

    void RotateObject()
    {
        RotatingObject.transform.RotateAround(around.position, new Vector3(0,0,1), rotateAmmount * Time.deltaTime);
    }

    void StopRotation()
    {
        CancelInvoke("RotateObject");
    }
}

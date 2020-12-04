using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Camera cam;

    public float ShakeAmmount;

    

    // Start is called before the first frame update
    
    void FixedUpdate()
    {
        
    }

    private void Awake() {
        cam = GetComponent<Camera>();   
    }

    public void Shake(float length)
    {
        
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("EndShake", length);
    }

    void BeginShake()
    {
        if(ShakeAmmount > 0)
        {
            Vector3 camPos = cam.transform.position;

            float offsetX = Random.value * ShakeAmmount * 2 - ShakeAmmount;
            float offsetY = Random.value * ShakeAmmount * 2 - ShakeAmmount;

            camPos.x += offsetX;
            camPos.y += offsetY;

            cam.transform.position = camPos;

        }
    }

    void EndShake()
    {
        CancelInvoke("BeginShake");
        cam.transform.localPosition = Vector3.zero;
    }

}

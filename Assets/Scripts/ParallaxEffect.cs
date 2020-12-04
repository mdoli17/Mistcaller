using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    public Transform cam;
    public float smoothnes;
    // Start is called before the first frame update
    
    public Rigidbody2D playerController;
    
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if(playerController.velocity.x > 0)
            transform.position = new Vector2(transform.position.x - smoothnes, transform.position.y);
        else if(playerController.velocity.x < 0)
            transform.position = new Vector2(transform.position.x + smoothnes, transform.position.y);
    }
}

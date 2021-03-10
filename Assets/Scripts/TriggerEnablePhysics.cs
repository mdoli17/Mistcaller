using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnablePhysics : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ObjectToRotate;

    public int jumpAmmount;
    private bool canbreak;

    void Start()
    {
        canbreak = false;
        // animator.StopPlayback();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            
            Controller2D controller = other.gameObject.GetComponent<Controller2D>();
            if(!controller.collisions.below)
            {
                Debug.Log(jumpAmmount);
                jumpAmmount--;
            }
                
            
            if(jumpAmmount == 0)
            {
                ObjectToRotate.transform.Rotate(new Vector3(0, 0, -35));
            }
        }
    }

 
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

        }
    }

    
}

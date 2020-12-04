using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnablePhysics : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objects;

    public int jumpAmmount;
    private bool canbreak;

    public Animator animator;
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
        Debug.Log(other.name);
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Controller2D controller = other.gameObject.GetComponent<Controller2D>();
            if(!controller.collisions.below)
            {
                jumpAmmount--;
            }
                
            
            if(jumpAmmount == 0)
            {
                foreach(GameObject o in objects)
                {
                    o.GetComponent<Rigidbody2D>().isKinematic = false;
                    o.GetComponent<Rigidbody2D>().mass = 10;
                }
            }
        }
    }

 
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            animator.SetBool("Jumped", false);
        }
    }

}

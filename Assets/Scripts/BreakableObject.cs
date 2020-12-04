using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    bool isBroken = false;
    public GameObject SpawnObject;

    public Vector2 SpawnLocation;
    // Start is called before the first frame update



    public Sprite Replacement;



    AudioSource sound;

    void Start() 
    {    
        sound = GetComponent<AudioSource>();
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(SpawnLocation, .3f);
    }




    public void Break()
    {
        if(isBroken) return;

        GetComponent<SpriteRenderer>().sprite = Replacement;
        Instantiate(SpawnObject, SpawnLocation, Quaternion.identity);

        sound.PlayOneShot(sound.clip);

        isBroken = true;
        
    }

    
}

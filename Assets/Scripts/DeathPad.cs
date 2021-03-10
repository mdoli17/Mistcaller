using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathPad : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Player Entered");
            player = other.gameObject;
            StartCoroutine(ResetPlayer());
        }
    }

    IEnumerator ResetPlayer()
    {
        player.GetComponent<Player>().enabled = false;
        yield return new WaitForSeconds(2);
        player.GetComponent<Player>().enabled = true;
        GameManager.ResetLevel();
    }
}

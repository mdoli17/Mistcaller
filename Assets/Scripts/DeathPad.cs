using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class DeathPad : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       GetComponent<PolygonCollider2D>().isTrigger = true;
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Player Entered");
            player = other.gameObject;
            Game.SoundManager.PlaySound(SoundNames.Game.SpikeDeathSound);
            StartCoroutine(ResetPlayer());
        }
    }

    IEnumerator ResetPlayer()
    {
        player.GetComponent<NewPlayerInput>().enabled = false;
        yield return new WaitForSeconds(2);
        player.GetComponent<NewPlayerInput>().enabled = true;
        player = null;
        GameManager.ResetLevel();
    }
}

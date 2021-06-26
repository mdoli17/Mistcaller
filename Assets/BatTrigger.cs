using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class BatTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            transform.parent.GetComponent<Animator>().SetTrigger("Player Enter Trigger");   
            Game.SoundManager.PlaySound(SoundNames.Environment.SpikeBallImpact);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Lever : MonoBehaviour, Interactable
{
    public delegate void LeverDelegate();
    public LeverDelegate LeverInteractDelegate;

    private BoxCollider2D playerChecker;

    private Player player;
    void Start()
    {
        playerChecker = GetComponent<BoxCollider2D>();
        playerChecker.isTrigger = true;
    }

    public void Interact()
    {
        if(LeverInteractDelegate != null)
        {
            LeverInteractDelegate();
            player.SetPlayerState(PlayerState.IDLE);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = other.gameObject.GetComponent<Player>();
            Debug.Log("Player Entered");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = null;
        }
    }
}

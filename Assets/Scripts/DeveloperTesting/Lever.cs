using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Lever : MonoBehaviour, Interactable
{
    public delegate void LeverDelegate();
    public LeverDelegate LeverInteractDelegate;

    private BoxCollider2D playerChecker;

    private AudioSource audioSource;

    private NewPlayer player;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void Interact()
    {
        player = FindObjectOfType<NewPlayer>();
        if(player.GetPlayerState() == PlayerState.IDLE) {
            Debug.Log("Interacted");
            audioSource.Play();
            if(LeverInteractDelegate != null)
            {
                LeverInteractDelegate();
            }
            player.SetPlayerState(PlayerState.INTERACTING);
            StartCoroutine(ResetInteraction());
        }
    }


    IEnumerator ResetInteraction() {
        yield return new WaitForSeconds(1f);
        player.SetPlayerState(PlayerState.IDLE);
        player = null;
    }
}

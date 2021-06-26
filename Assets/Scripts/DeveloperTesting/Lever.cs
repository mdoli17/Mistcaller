using System.Collections;
using System.Collections.Generic;
using Game;
using Helpers;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Lever : MonoBehaviour, Interactable
{
    public delegate void LeverDelegate();
    public LeverDelegate LeverInteractDelegate;

    private BoxCollider2D playerChecker;

    private AudioSource audioSource;

    private NewPlayer player;
    private Animator leverAnimator;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        leverAnimator = GetComponent<Animator>();
    }

    public void Interact()
    {
        player = FindObjectOfType<NewPlayer>();
        if(player.GetPlayerState() == PlayerState.IDLE) {
            audioSource.Play();
            leverAnimator.SetTrigger(AnimationInfos.Trigger.ClickAnimationTrigger);
            if(LeverInteractDelegate != null)
            {
                LeverInteractDelegate();
            }
            player.SetPlayerState(PlayerState.INTERACTING);
            StartCoroutine(ResetInteraction());
            // Game.SoundManager.PlaySound(SoundNames.Environment.ButtonPressSound);
        }
    }


    IEnumerator ResetInteraction() {
        yield return new WaitForSeconds(1f);
        player.SetPlayerState(PlayerState.IDLE);
        player = null;
    }
}

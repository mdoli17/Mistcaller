using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class FishPuzzle : MonoBehaviour
{
    public Lever ropeLever;
    public Lever spikeLever;
    public Lever cageLever;

    public GameObject ropeObject;
    public GameObject spikeObject;
    public GameObject cageObject;
    public GameObject additionalRopeObject;
    public Transform cageTargetLocation;

    public Animator animator;
    private void Start()
    {
        ropeLever.LeverInteractDelegate += OnRopeLever;
        spikeLever.LeverInteractDelegate += OnSpikeLever;
        cageLever.LeverInteractDelegate += OnCageLever;
    }

    private void OnRopeLever()
    {
        ropeObject.SetActive(!ropeObject.activeSelf);
    }

    private void OnSpikeLever()
    {
        spikeObject.SetActive(!spikeObject.activeSelf);
        additionalRopeObject.SetActive(!additionalRopeObject.activeSelf);
    }

    private void OnCageLever()
    {
        animator.SetBool("CageDropped", true);
        cageObject.transform.position = cageTargetLocation.transform.position;
        Game.SoundManager.PlaySound(SoundNames.Environment.FishGateFallSound);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        animator.SetTrigger("PlayerEntered");
        Game.SoundManager.PlaySound(SoundNames.Environment.FishBiteSound);
        Game.SoundManager.PlaySound(SoundNames.Environment.FishPullUpFromWaterSound);
    }
}

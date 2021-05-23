using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class TimerPuzzle : MonoBehaviour
{
    private int lettersFound = 0;

    public Lever first;
    public Lever second;
    public Lever third;
    
    public SpriteRenderer spriteFirst, spriteSecond, spriteThird;

    public AbilityDisabler disabler;

    public PlatformTrigger platform;

    public Animator doorAnimator;
    [SerializeField] private float closeDoorIn;
    private void Awake()
    {
        first.LeverInteractDelegate += OnFirstInteract;
        second.LeverInteractDelegate += OnSecondInteract;
        third.LeverInteractDelegate += OnThirdInteract;
        platform.onObstaclePressed += OnPlatformObstaclePressed;
        platform.onPlayerPressed += OnPlatformPlayerPressed;
    }

    private void OnFirstInteract()
    {
        lettersFound++;
        first.GetComponent<Renderer>().enabled = false;
        first.LeverInteractDelegate -= OnFirstInteract;
        spriteFirst.gameObject.SetActive(false);
        OnInteract();
    }

    private void OnSecondInteract()
    {
        lettersFound++;
        second.GetComponent<Renderer>().enabled = false;
        second.LeverInteractDelegate -= OnFirstInteract;
        spriteSecond.gameObject.SetActive(false);
        OnInteract();
    }

    private void OnThirdInteract()
    {
        lettersFound++;
        third.GetComponent<Renderer>().enabled = false;
        third.LeverInteractDelegate -= OnFirstInteract;
        spriteThird.gameObject.SetActive(false);
        OnInteract();
    }

    private void OnInteract()
    {
        if (lettersFound == 3)
        {
            disabler.gameObject.SetActive(false);
        }
    }

    private void OnPlatformObstaclePressed()
    {
        doorAnimator.SetTrigger(AnimationInfos.Door.OpenAnimationTrigger);
    }
    
    private void OnPlatformPlayerPressed()
    {
        doorAnimator.SetTrigger(AnimationInfos.Door.OpenAnimationTrigger);
        StartCoroutine(CloseDoorInTime());
    }

    private IEnumerator CloseDoorInTime()
    {
        yield return new WaitForSeconds(closeDoorIn);
        doorAnimator.SetTrigger(AnimationInfos.Door.CloseAnimationTrigger);
    }
}

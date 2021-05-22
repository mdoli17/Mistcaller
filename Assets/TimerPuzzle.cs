using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerPuzzle : MonoBehaviour
{
    private int lettersFound = 0;

    public Lever first;
    public Lever second;
    public Lever third;

    public SpriteRenderer spriteFirst, spriteSecond, spriteThird;

    public AbilityDisabler disabler;

    private void Awake()
    {
        first.LeverInteractDelegate += OnFirstrInteract;
        second.LeverInteractDelegate += OnSecondInteract;
        third.LeverInteractDelegate += OnThirdInteract;
    }

    public void OnFirstrInteract()
    {
        lettersFound++;
        first.GetComponent<Renderer>().enabled = false;
        first.LeverInteractDelegate -= OnFirstrInteract;
        spriteFirst.gameObject.SetActive(false);
        OnInteract();
    }

    public void OnSecondInteract()
    {
        lettersFound++;
        second.GetComponent<Renderer>().enabled = false;
        second.LeverInteractDelegate -= OnFirstrInteract;
        spriteSecond.gameObject.SetActive(false);
        OnInteract();
    }

    public void OnThirdInteract()
    {
        lettersFound++;
        third.GetComponent<Renderer>().enabled = false;
        third.LeverInteractDelegate -= OnFirstrInteract;
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
}

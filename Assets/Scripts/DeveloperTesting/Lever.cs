using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider2D))]
public class Lever : MonoBehaviour, Interactable
{
    public delegate void LeverDelegate();
    public LeverDelegate LeverInteractDelegate;

    private BoxCollider2D playerChecker;

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
        }
    }
}

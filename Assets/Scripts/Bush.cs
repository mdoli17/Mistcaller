using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BerryColour
{
    Red,
    Blue,
    Black
}

public struct Berry
{
    public BerryColour berryColour;

    public Berry(BerryColour color)
    {
        berryColour = color;
    }
}

[RequireComponent (typeof (BoxCollider2D))]
public class Bush : MonoBehaviour, Interactable
{
    public BerryColour berryColour;

    Berry berry;

    BoxCollider2D trigger;
    
    void Start()
    {
        trigger = GetComponent<BoxCollider2D>();
        trigger.isTrigger = true;
        berry = new Berry(berryColour);
    }

    public void Interact()
    {
        Debug.Log("Interacting with BerryBush of color " + berry.berryColour);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryPuzzle : MonoBehaviour
{
    public Lever red;
    public Lever green;
    public Lever yellow;
    public Lever pot;

    public GameObject door;

    private const string TargetCommand = "RPGPYP";

    private string current = "";
    private string color;

    public Sprite Red;
    public Sprite Green;
    public Sprite Yellow;
    
    private void Start() {
        red.LeverInteractDelegate += onRed;
        green.LeverInteractDelegate += onGreen;
        yellow.LeverInteractDelegate += onYellow;
        pot.LeverInteractDelegate += onPot;
    }

    private void onRed()
    {   
        color = "R";
        SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Red;
        renderer.drawMode = SpriteDrawMode.Sliced;
        renderer.size = new Vector2(0.3f,0.25f);
    }

    private void onGreen()
    {
        color = "G";
        SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Green;
        renderer.drawMode = SpriteDrawMode.Sliced;
        renderer.size = new Vector2(0.3f,0.25f);
    }

    private void onYellow() 
    {
        color = "Y";
        SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Yellow;
        renderer.drawMode = SpriteDrawMode.Sliced;
        renderer.size = new Vector2(0.3f,0.25f);
    } 
    private void onPot() 
    {
        current += color + 'P';
        color = "";
        SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = null;  
        CheckCondition();
    }     

    private void CheckCondition()
    {
        if (current.Length == TargetCommand.Length)
        {
            if (current == TargetCommand)
            {
                Destroy(door);
            }
            else {

            }
            current = "";
        }
    }
}

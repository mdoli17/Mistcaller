using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryPuzzle : MonoBehaviour
{
    public Lever red;
    public Lever blue;
    public Lever yellow;
    public Lever pot;
    public Lever owl;

    public GameObject door;

    private const string TargetCommand = "RPGPYP";

    private string current = "";
    private string potColor;
    private string _playerCurrentColor = "";
    private string _owlColor = "";
    
    public Sprite Red;
    public Sprite Green;
    public Sprite Yellow;
    public Sprite Orange;
    public Sprite Purple;
    public Sprite Blue;

    
    
    private void Start() {
        red.LeverInteractDelegate += onRed;
        blue.LeverInteractDelegate += onBlue;
        yellow.LeverInteractDelegate += onYellow;
        pot.LeverInteractDelegate += onPot;
        owl.LeverInteractDelegate += onOwl;
    }
    
    private void onRed()
    {   
        _playerCurrentColor = "R";
        SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Red;
        renderer.drawMode = SpriteDrawMode.Sliced;
        renderer.size = new Vector2(0.3f,0.25f);
    }

    private void onBlue()
    {
        _playerCurrentColor = "B";
        SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Blue;
        renderer.drawMode = SpriteDrawMode.Sliced;
        renderer.size = new Vector2(0.3f,0.25f);
    }

    private void onYellow() 
    {
        _playerCurrentColor = "Y";
        SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Yellow;
        renderer.drawMode = SpriteDrawMode.Sliced;
        renderer.size = new Vector2(0.3f,0.25f);
    } 
    private void onPot()
    {
        potColor += _playerCurrentColor;
        _playerCurrentColor = "";
        if (potColor.Length == 2)
        {
            SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
            switch (potColor)
            {
                case "RB":
                case "BR":
                    renderer.sprite = Purple;
                    _playerCurrentColor = "P";
                    break;
                case "RY":
                case "YR":
                    renderer.sprite = Orange;
                    _playerCurrentColor = "O";
                    break;
                case "BY":
                case "YB":
                    renderer.sprite = Green;
                    _playerCurrentColor = "G";
                    break;
                
            }
            renderer.drawMode = SpriteDrawMode.Sliced;
            renderer.size = new Vector2(0.3f,0.25f);
            
            potColor = "";
        }
        else
        {
            SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
            renderer.sprite = null;
        }
    }

    private void onOwl()
    {
        _owlColor += _playerCurrentColor;
        _playerCurrentColor = "";
        if (_owlColor.Length == 2)
        {
            switch (_owlColor)
            {
                case "PG":
                case "GP":
                    onPuzzleComplete();
                    break;
                default:
                    _owlColor = "";
                    break;
            }
        }
        SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = null;
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

    private void onPuzzleComplete()
    {
        door.SetActive(false);
    }
}

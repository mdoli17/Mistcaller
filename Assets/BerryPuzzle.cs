using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

public class BerryPuzzle : MonoBehaviour
{
    public Animator spikeAnimator;
    
    public Lever red;
    public Lever blue;
    public Lever yellow;
    public Lever pot;
    public Lever owl;

    public Lever caveLever;
    public GameObject caveDoor;
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

    private Dictionary<char, Sprite> colors;

    public SpriteRenderer owlLeftEye;
    public SpriteRenderer owlRightEye;
    
    private void Start() {
        red.LeverInteractDelegate += onRed;
        blue.LeverInteractDelegate += onBlue;
        yellow.LeverInteractDelegate += onYellow;
        pot.LeverInteractDelegate += onPot;
        owl.LeverInteractDelegate += onOwl;
        caveLever.LeverInteractDelegate += OnCaveOpen;

        colors = new Dictionary<char, Sprite>();
        colors.Add('R', Red);
        colors.Add('G', Green);
        colors.Add('B', Blue);
        colors.Add('Y', Yellow);
        colors.Add('O', Orange);
        colors.Add('P', Purple);
        
    }

    private void OnCaveOpen()
    {
        caveDoor.SetActive(false);
        caveLever.LeverInteractDelegate -= OnCaveOpen;
    }
    private void onRed()
    {   
        _playerCurrentColor = "R";
        SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Red;
        renderer.drawMode = SpriteDrawMode.Sliced;
        renderer.size = new Vector2(0.3f,0.25f);

        var index = Random.Range(0, 3);
        switch (index)
        {
            case 1:
                Game.SoundManager.PlaySound(SoundNames.Environment.BerryPickOne);
                break;
            case 2:
                Game.SoundManager.PlaySound(SoundNames.Environment.BerryPickTwo);
                break;
            case 3:
                Game.SoundManager.PlaySound(SoundNames.Environment.BerryPickThree);
                break;
        }
    }

    private void onBlue()
    {
        _playerCurrentColor = "B";
        SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Blue;
        renderer.drawMode = SpriteDrawMode.Sliced;
        renderer.size = new Vector2(0.3f,0.25f);
        var index = Random.Range(0, 3);
        switch (index)
        {
            case 1:
                Game.SoundManager.PlaySound(SoundNames.Environment.BerryPickOne);
                break;
            case 2:
                Game.SoundManager.PlaySound(SoundNames.Environment.BerryPickTwo);
                break;
            case 3:
                Game.SoundManager.PlaySound(SoundNames.Environment.BerryPickThree);
                break;
        }
    }

    private void onYellow() 
    {
        _playerCurrentColor = "Y";
        SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Yellow;
        renderer.drawMode = SpriteDrawMode.Sliced;
        renderer.size = new Vector2(0.3f,0.25f);
        var index = Random.Range(0, 3);
        switch (index)
        {
            case 1:
                Game.SoundManager.PlaySound(SoundNames.Environment.BerryPickOne);
                break;
            case 2:
                Game.SoundManager.PlaySound(SoundNames.Environment.BerryPickTwo);
                break;
            case 3:
                Game.SoundManager.PlaySound(SoundNames.Environment.BerryPickThree);
                break;
        }
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
        Game.SoundManager.PlaySound(SoundNames.Environment.PotSplashSound);
    }

    private void onOwl()
    {
        _owlColor += _playerCurrentColor;
        switch (_owlColor.Length)
        {
            case 1:
                owlLeftEye.sprite = colors[_owlColor[0]];
                break;
            case 2:
                owlRightEye.sprite = colors[_owlColor[1]];
                break;
        }
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
                    StartCoroutine(ResetOwl());
                    _owlColor = "";
                    break;
            }
            
        }
        SpriteRenderer renderer = FindObjectOfType<InteractChecker>().gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = null;
        Game.SoundManager.PlaySound(SoundNames.Environment.OwlSound);
    }
    

    private IEnumerator ResetOwl()
    {
        yield return new WaitForSeconds(1f);
        owlLeftEye.sprite = null;
        owlRightEye.sprite = null;
    }
    
    private void onPuzzleComplete()
    {
        door.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            spikeAnimator.SetTrigger(AnimationInfos.Spike.SpikeFallTrigger);
            Game.SoundManager.PlaySound(SoundNames.Environment.SpikeFallSound);
        }
    }
}

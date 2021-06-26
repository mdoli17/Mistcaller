using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class BellPuzzle : MonoBehaviour
{
    public Lever high;
    public Lever mid;
    public Lever low;

    public Animator lowNoteAnimator;
    public Animator midNoteAnimator;
    public Animator highNoteAnimator;
    
    public ElevatorController elevator;

    private const string TargetCommand = "MHLH";

    private string current = "";
    public SpriteRenderer onFailImage;

    private void Start() {
        high.LeverInteractDelegate += onHigh;
        mid.LeverInteractDelegate += onMid;
        low.LeverInteractDelegate += onLow;
    }

    private void onHigh()
    {   
        current += "H";
        CheckCondition();
        if(highNoteAnimator) highNoteAnimator.SetTrigger("Note Player");
        Game.SoundManager.PlaySound(SoundNames.Environment.DrumHighSound);
    }

    private void onMid()
    {
        current += "M";
        CheckCondition();
        if(midNoteAnimator) midNoteAnimator.SetTrigger("Note Player");
        Game.SoundManager.PlaySound(SoundNames.Environment.DrumMidSound);

    }

    private void onLow() 
    {
        current += "L";
        CheckCondition();
        if (lowNoteAnimator) lowNoteAnimator.SetTrigger("Note Player");
        Game.SoundManager.PlaySound(SoundNames.Environment.DrumLowSound);
    }   

    private void CheckCondition()
    {
        if (current.Length > TargetCommand.Length)
            current = current.Substring(1);

        if (current == TargetCommand)
        {
            elevator.ResetPlatform();
            Game.SoundManager.PlaySound(SoundNames.Environment.ElevatorBackgroundMusic);
        }
            

    }
}

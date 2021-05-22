using System.Collections;
using System.Collections.Generic;
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
    }

    private void onMid()
    {
        current += "M";
        CheckCondition();
        if(midNoteAnimator) midNoteAnimator.SetTrigger("Note Player");

    }

    private void onLow() 
    {
        current += "L";
        CheckCondition();
        if (lowNoteAnimator) lowNoteAnimator.SetTrigger("Note Player");
    }   

    private void CheckCondition()
    {
        if (current.Length == TargetCommand.Length)
        {
            if (current == TargetCommand)
            {
                elevator.ResetPlatform();
            }
            onFailImage.gameObject.SetActive(true);
            StartCoroutine(DeactivateIn(0.3f));
            current = "";
        }
    }

    IEnumerator DeactivateIn(float time)
    {
        yield return new WaitForSeconds(time);
        onFailImage.gameObject.SetActive(false);
    }
}

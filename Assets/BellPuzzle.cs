using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellPuzzle : MonoBehaviour
{
    public Lever high;
    public Lever mid;
    public Lever low;

    public PlatformController elevator;

    private const string TargetCommand = "MLH";

    private string current = "";


    private void Start() {
        high.LeverInteractDelegate += onHigh;
        mid.LeverInteractDelegate += onMid;
        low.LeverInteractDelegate += onLow;
    }

    private void onHigh()
    {   
        current += "H";
        CheckCondition();
    }

    private void onMid()
    {
        current += "M";
        CheckCondition();
    }

    private void onLow() 
    {
        current += "L";
        CheckCondition();
    }   

    private void CheckCondition()
    {
        if (current.Length == TargetCommand.Length)
        {
            if (current == TargetCommand)
            {
                elevator.ResetPlatform();
            }
            current = "";
        }
    }
}

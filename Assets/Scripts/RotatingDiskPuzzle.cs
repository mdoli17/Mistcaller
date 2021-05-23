
using System;
using Helpers;
using UnityEngine;

public class RotatingDiskPuzzle : MonoBehaviour
{
    public Lever leftLever;
    public Lever rightLever;

    public Animator diskAnimator;

    public GameObject doorToDisable;

    private const string TargetValue = "RRLLLLRRR";
    private string currentValue = "";
    
    private void Awake()
    {
        leftLever.LeverInteractDelegate += OnLeftLever;
        rightLever.LeverInteractDelegate += OnRightLever;
    }

    private void OnLeftLever()
    {
        currentValue += 'L';
        CheckCondition();
        diskAnimator.SetTrigger(AnimationInfos.RotatingDisk.LeftAnimationTrigger);
    }

    private void OnRightLever()
    {
        currentValue += 'R';
        CheckCondition();
        diskAnimator.SetTrigger(AnimationInfos.RotatingDisk.RightAnimationTrigger);
    }

    private void CheckCondition()
    {
        if (currentValue.Length == TargetValue.Length)
        {
            if (currentValue == TargetValue)
            {
                rightLever.LeverInteractDelegate -= OnRightLever;
                leftLever.LeverInteractDelegate -= OnLeftLever;
                doorToDisable.SetActive(false);
            }
            else
            {
                currentValue = "";
                diskAnimator.SetTrigger(AnimationInfos.RotatingDisk.IncorrectAnimationTrigger);
            }
        }
    }
}

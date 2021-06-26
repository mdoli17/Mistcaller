using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class RotatingPuzzleMaster : MonoBehaviour
{
    public RotatingPuzzleObject[] objects;

    public Animator doorAnimator;

    public int target;

    public void checkPuzzle()
    {
        foreach (RotatingPuzzleObject obj in objects)
        {
            Debug.Log(obj.name + "Number: " + obj.getCurrentNumber());
            if (obj.getCurrentNumber() != target)
                return;
        }

        OnPuzzleSolve();
    }

    private void OnPuzzleSolve()
    {
        Debug.Log("Puzzle Solved");
        doorAnimator.SetTrigger("Door Opened");
        Game.SoundManager.PlaySound(SoundNames.Environment.DoorOpenSound);
    }
}

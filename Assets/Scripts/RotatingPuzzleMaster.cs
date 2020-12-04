using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPuzzleMaster : MonoBehaviour
{
    public RotatingPuzzleObject[] objects;

    public int target;

    public void checkPuzzle()
    {
        foreach (RotatingPuzzleObject obj in objects)
        {
            if (obj.getCurrentNumber() != target)
                return;
        }

        OnPuzzleSolve();
    }

    private void OnPuzzleSolve()
    {
        Debug.Log("Puzzle Solved");
    }
}

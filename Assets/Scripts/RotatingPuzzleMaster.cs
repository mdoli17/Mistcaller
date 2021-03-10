using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPuzzleMaster : MonoBehaviour
{
    public RotatingPuzzleObject[] objects;
    public BoxCollider2D door;
    public GameObject doorSprite;

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
        if (door)
        {
            door.enabled = false;
            doorSprite.SetActive(false);
        }
    }
}

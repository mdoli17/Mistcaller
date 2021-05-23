using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor;

public class RotatingPuzzleObject : MonoBehaviour
{
    private int Number;
    public Transform[] positions;
    public Lever lever;

    public RotatingPuzzleObject[] objects;
    void Start()
    {
        lever.LeverInteractDelegate += leverFunction;
    }

    // void OnDrawGizmos()
    // {
    //     Handles.Label(new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Number.ToString());
    // }

    public void Increment()
    {
        Number++;
        Number %= 3;
        transform.position = new Vector3(transform.position.x, positions[Number].position.y, transform.position.z);
        
    }

    void leverFunction()
    {
        
        foreach (RotatingPuzzleObject obj in objects)
        {
            
            obj.Increment();
        }
        GetComponentInParent<RotatingPuzzleMaster>().checkPuzzle();
    }

    public int getCurrentNumber()
    {
        return Number;
    }
}

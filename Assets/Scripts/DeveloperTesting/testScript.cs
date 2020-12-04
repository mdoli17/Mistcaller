using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public Lever lever;

    void Start()
    {
        lever.LeverInteractDelegate += LeverFunction;
    }

    void LeverFunction()
    {
        Debug.Log("Gamo Shuqze");
    }
}

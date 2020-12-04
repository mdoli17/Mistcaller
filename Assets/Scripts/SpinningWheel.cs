using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpinningWheel : MonoBehaviour
{
    public float rotatingSpeed = 1f;

    [Range(0,1)]
    public float slowAmmount = 1f;

    public Lever lever;

    private Vector3 speed;

    private bool bSwitch;

    void Start()
    {
        lever.LeverInteractDelegate += onLeverInteract;
        bSwitch = true;
        speed = new Vector3(0, 0, rotatingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed);
    }

    void onLeverInteract()
    {
        if(bSwitch)
        {
            speed *= slowAmmount;
            bSwitch = false;
        }
        else
        {
            speed = new Vector3(0,0,rotatingSpeed);
            bSwitch = true;
        }
    }
    
}

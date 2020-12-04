using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class BushPuzzleMaster : MonoBehaviour, Interactable
{
    public BerryColour[] targetColours;

    public BerryColour[] currentColours;


    // Start is called before the first frame update
    void Start()
    {

        checkResult();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        
    }

    void checkResult()
    {
        if(currentColours.Length == targetColours.Length)
        {
            for(int i = 0; i < targetColours.Length; i++)
            {
                if(targetColours[i] != currentColours[i])
                {
                    Debug.Log("Wrong Order");
                    currentColours = new BerryColour[0];
                    return;
                }
            }

            Debug.Log("Correct Order");

        }
    }    
}

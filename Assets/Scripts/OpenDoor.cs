using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Lever lever;

    // Start is called before the first frame update
    void Start()
    {
        lever.LeverInteractDelegate += leverFunction;        
    }




    void leverFunction()
    {
        Debug.LogError("AEAEAEAAE");
        Destroy(this.gameObject);
    }
}

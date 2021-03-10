using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent (typeof(BoxCollider2D))]
public class RotationalHandle : MonoBehaviour, Interactable
{

    public delegate void SafeCodeDelegate();
    public SafeCodeDelegate onCodeSuccess;
    private Player player;

    public float rotationTime;
    private BoxCollider2D collider;
    
    private Collider2D otherCollider;

    private bool canBeRotated = true;

    public string  WinCode = "LLRRR";
    private string curCode = "";

    public BoxCollider2D door;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        // collider.isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    private void Update() {
        if(player)
        {
            if(player.GetPlayerState() == PlayerState.INTERACTING)
            {
                if(canBeRotated) 
                {
                    if(Input.GetKeyDown(KeyCode.A))
                        StartCoroutine(RotateLock("L"));
                else if (Input.GetKeyDown(KeyCode.D))
                        StartCoroutine(RotateLock("R"));
                }
            } 
        }  
    }


    public void Interact()
    {
        player = FindObjectOfType<Player>();
        curCode = "";
        if (player.GetPlayerState() == PlayerState.IDLE)
            player.SetPlayerState(PlayerState.INTERACTING);
        else
        {
            player.SetPlayerState(PlayerState.IDLE);
            curCode = "";
            player = null;
        }
    }

    IEnumerator RotateLock(string code)
    {
        canBeRotated = false;

        yield return new WaitForSeconds(rotationTime);

        curCode += code;
        Debug.Log(curCode);
        if(curCode == WinCode)
        {
            Debug.Log("CORRECT CODE");
            
            door.gameObject.SetActive(false);   
            player.SetPlayerState(PlayerState.IDLE);

        }
        else if(curCode.Length == WinCode.Length)
        {
            player.SetPlayerState(PlayerState.IDLE);
            curCode = "";
            player = null;
            Debug.Log("WRONG CODE");
        }
        canBeRotated = true;
    }

    private void OnDrawGizmos() {

    }
}

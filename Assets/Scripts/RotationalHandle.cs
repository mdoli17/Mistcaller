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

    private const string  WinCode = "LLRRR";
    private string curCode = "";

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = (Player) other.gameObject.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = null;
        }
    }

    public void Interact()
    {
        curCode = "";
    }

    IEnumerator RotateLock(string code)
    {
        canBeRotated = false;
        Debug.Log("Rotation Locked");
        yield return new WaitForSeconds(rotationTime);
        Debug.Log("Rotated");
        curCode += code;
        if(curCode == WinCode)
        {
            Debug.Log("CORRECT CODE");
            onCodeSuccess();

        }
        else if(curCode.Length == WinCode.Length)
        {
            curCode = "";
            Debug.Log("WRONG CODE");
        }
        canBeRotated = true;
    }

    private void OnDrawGizmos() {
        Handles.Label(transform.position + new Vector3(0,7,0), WinCode);
        Handles.Label(transform.position + new Vector3(0,5,0), curCode);
    }
}

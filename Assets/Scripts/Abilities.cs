using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Abilities
{

    

    // public static void MistyJump(Controller MistController)
    // {
    //     if(MistController.bCanDetectMist() && MistController.MistyJumpsAvailable())
    //     {
    //         MistController.MistyBody.velocity = new Vector2(MistController.MistyBody.velocity.x, MistController.JumpForce / 50);
    //         MistController.UseMistyJump();
    //     }
    // }

    public static GameObject Hook()
    {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 MousePosition = new Vector2(pz.x, pz.y);

            int layerMask = LayerMask.GetMask("Mist");

            RaycastHit2D hit2D = Physics2D.Raycast(MousePosition, Vector2.zero, Mathf.Infinity, layerMask);
            if(hit2D.collider)
            {
                if(hit2D.collider.gameObject.layer == LayerMask.NameToLayer("Mist"))
                {
                    return hit2D.collider.gameObject;
                    
                }
            }
            else
            {
                // Debug.Log("Nothing Was Hit");
            }
            return null;
    }


    public static void EndHook(GameObject StartObject)
    {
        Vector3 StartPosition = StartObject.transform.position;

        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 MousePosition = new Vector2(pz.x, pz.y);

        int layerMask = LayerMask.GetMask("Enemy", "Movable");

        RaycastHit2D hit2D = Physics2D.Raycast(MousePosition, Vector2.zero, Mathf.Infinity, layerMask);
        
        if(hit2D.collider)
        {
            GameObject TargetObject = hit2D.collider.gameObject;

            Debug.Log(TargetObject);

            if(TargetObject.layer == LayerMask.NameToLayer("Enemy"))
            {    
                HookEnemy(TargetObject, StartObject, StartPosition);
            }
            else if(TargetObject.layer == LayerMask.NameToLayer("Movable"))
            {
                Rigidbody2D TargetBody = TargetObject.GetComponent<Rigidbody2D>();
                if(TargetBody)
                {
                    Vector2 Direction = (StartObject.transform.position - TargetObject.transform.position).normalized;
                    TargetBody.AddForce(Direction * 20);
                }
            }
        }
        else
        {
            // Debug.Log("Nothing Was Hit");
        }
    }


    private static void HookEnemy(GameObject TargetObject, GameObject StartObject, Vector3 StartPosition)
    {
        // Debug.Log("Enemy Detected");

        // Rigidbody2D TargetBody = TargetObject.GetComponent<Rigidbody2D>();
        // if(TargetBody)
        // {
        //     TargetBody.gravityScale = 0;
        //     Vector2 Direction = (StartObject.transform.position - TargetObject.transform.position).normalized;
        //     TargetObject.GetComponent<EnemyScript>().SetGotHooked(StartPosition);
        //     TargetBody.AddForce(Direction * 1000);

        // }
        // else
        // {
        //     Debug.Log("No RigidBody2D Detected");
        // }
    }

    public static bool CanDetectMist()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 MousePosition = new Vector2(pz.x, pz.y);

        int layerMask = LayerMask.GetMask("Mist");

        RaycastHit2D hit2D = Physics2D.Raycast(MousePosition, Vector2.zero, Mathf.Infinity, layerMask);
        if(hit2D.collider)
        {
            if(hit2D.collider.gameObject.layer == LayerMask.NameToLayer("Mist"))
            {
                return true;
            }
        }
        return false;
    }


}

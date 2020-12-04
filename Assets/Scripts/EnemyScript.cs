using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    bool WasHooked;
    Vector2 HookStartPosition;

    public float HookDetectOffset;
    // Start is called before the first frame update
    void Start()
    {
        WasHooked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(WasHooked)
        {
            OnReachHook();
        }
    }

    void OnReachHook()
    {
        Vector2 myPosition = new Vector2(transform.position.x, transform.position.y);
        if(Vector2.Distance(myPosition, HookStartPosition) < HookDetectOffset)
        {
            Destroy(gameObject);
        }
    }

    public void SetGotHooked(Vector2 HookStartPosition)
    {
        this.HookStartPosition = HookStartPosition;
        WasHooked = true;
    }
}

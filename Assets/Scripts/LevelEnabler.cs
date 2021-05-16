using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (BoxCollider2D))]
public class LevelEnabler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] last;
    [SerializeField]
    private GameObject[] next;
    
    bool firstTime = true;

    public bool disableLevels;
    private BoxCollider2D collider;
    private void Start() {
        collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(collider.size.x, 60);
        if(disableLevels)
        {
            foreach (var VARIABLE in next)
            {
                if(VARIABLE && VARIABLE.name != "Scene 03.1 Lodi") VARIABLE.SetActive(false);
                else if(VARIABLE.name == "Scene 03.1 Lodi")
                {
                    StartCoroutine(disableInTime(VARIABLE));
                }
            }
        }
    }

    IEnumerator disableInTime(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (next.Length != 0)
            {
                foreach (var VARIABLE in next)
                {
                    VARIABLE.SetActive(VARIABLE.activeSelf ? false : true);
                }
            }
            if (last.Length != 0)
            {
                foreach (var VARIABLE in last)
                {
                    VARIABLE.SetActive(VARIABLE.activeSelf ? false : true);
                }
            }
            
        }
    }



}

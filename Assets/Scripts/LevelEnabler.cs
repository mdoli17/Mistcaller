using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (BoxCollider2D))]
public class LevelEnabler : MonoBehaviour
{
    [SerializeField]
    private GameObject last;
    [SerializeField]
    private GameObject next;
    
    bool firstTime = true;

    public bool disableLevels;
    private BoxCollider2D collider;
    private void Start() {
        collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(collider.size.x, 60);
        if(disableLevels)
        {
            if(next && next.name != "Scene 03.1 Lodi") next.SetActive(false);
            else if(next.name == "Scene 03.1 Lodi")
            {
                StartCoroutine(disableInTime(next));
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
            if(next) next.SetActive(next.activeSelf ? false : true);
            if(last) last.SetActive(last.activeSelf ? false : true);
        }
    }



}

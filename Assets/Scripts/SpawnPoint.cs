using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.size = new Vector2(2,10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SpawnPointManager manager = GetComponentInParent<SpawnPointManager>();
            manager.setLastSpawnPoint(transform.position);
        }        
    }
}

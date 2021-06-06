using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsToSave;

    private BoxCollider2D collider;
    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(collider.size.x, 60);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //GameManager.SaveGame();
            GameManager.SaveGame(objectsToSave);
            Destroy(gameObject);
        }
    }
}

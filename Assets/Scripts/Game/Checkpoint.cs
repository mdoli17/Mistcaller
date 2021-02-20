using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
    private const string saveFileName = "CheckpointSave.txt";
    SaveManager saveManager = SaveManager.shared;
    private BoxCollider2D collider;
    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SaveObject save = new SaveObject();
            save.PlayerPosition = other.gameObject.transform.position;
            saveManager.WriteToFile(save, saveFileName);
        }
    }
}

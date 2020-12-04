using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    
    public Vector2[] checkPoints;

    private static Vector2 lastSpawnPoint;
    void Start()
    {
        lastSpawnPoint = Vector2.zero;
        foreach(Vector2 point in checkPoints)
        {
            GameObject spawnObject = new GameObject("Spawn Point");
            spawnObject.transform.position = new Vector2(transform.position.x, transform.position.y) + point;
            spawnObject.AddComponent<SpawnPoint>();
            spawnObject.transform.parent = gameObject.transform;
        }
    }

    void OnDrawGizmos()
    {
        foreach(Vector2 point in checkPoints)
            Gizmos.DrawSphere(point + new Vector2(transform.position.x,transform.position.y), .4f);
    }

    public static Vector2 getLastSpawnPoint() { return lastSpawnPoint; }

    public void setLastSpawnPoint(Vector2 point) { lastSpawnPoint = point; }
}

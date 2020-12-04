using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float RespawnTime;
    public float MaxHealth;

    [SerializeField]
    float CurrentHealth;

    
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0.0f)
        {
            StartCoroutine(Respawn());
        }
    }

    public void RespawnPlayer()
    {

    }


    IEnumerator Respawn()
    {
        GetComponent<Controller>().enabled = false;
        
        yield return new WaitForSeconds(RespawnTime);
        
        transform.position = SpawnPointManager.getLastSpawnPoint();
        CurrentHealth = MaxHealth;

        GetComponent<Controller>().enabled = true;

    }
    
}

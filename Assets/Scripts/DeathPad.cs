using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPad : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //other.transform.gameObject.GetComponentInChildren<HealthComponent>().TakeDamage(150);
            StartCoroutine(ResetPlayer());
        }
    }

    IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(2);
        SaveObject save = SaveManager.shared.ReadFromFile("Quicksave.txt");
        gameObject.transform.position = save.PlayerPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ArrowMan : MonoBehaviour
{
    public GameObject arrow;
    private GameObject obj;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(FiringMode());
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(obj);
            StopAllCoroutines();
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator FiringMode()
    {
        yield return new WaitForSeconds(2.5f);
        obj = Instantiate(arrow);
        obj.transform.position = transform.position;
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(-35f,0);
        while(true)
        {   
            yield return new WaitForSeconds(3.5f);
            obj.transform.position = transform.position;    
        }
    }

}

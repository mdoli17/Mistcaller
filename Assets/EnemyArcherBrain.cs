using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherBrain : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject arrow;

    public float shootTime;
    public float MaxHeight;

    public float ShootInterval;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shootOnInterval());
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator shootOnInterval()
    {
        yield return new WaitForSeconds(ShootInterval);
        shootProjectile();
        StartCoroutine(shootOnInterval());
    }

    void shootProjectile()
    {
        arrow.transform.position = transform.position;
        arrow.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Vector3 direction = Vector3.zero;
        float distance = Mathf.Abs(transform.position.x - playerTransform.position.x);
        float tanAlpha = (MaxHeight - 9.8f * shootTime * shootTime / 2) / distance;
        float alpha = Mathf.Atan(tanAlpha);
        float speed = distance / shootTime;
        direction = new Vector3(Mathf.Cos(Mathf.Rad2Deg * alpha), Mathf.Sin(Mathf.Rad2Deg * alpha), 0) * speed;
        arrow.GetComponent<Rigidbody2D>().velocity = direction;
    }
}

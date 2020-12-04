using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAlphaContrller : MonoBehaviour
{
    // Start is called before the first frame update

    ParticleSystem particleSystem;
    public float smoothing;
    
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        StartCoroutine(UpdateAlpha());
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator UpdateAlpha()
    {
        while(particleSystem.main.startColor.color.a != 0.28f)
        {
            Color c = particleSystem.main.startColor.color;
            float newAlpha = Mathf.Lerp(0,0.28f,smoothing * Time.deltaTime);
            Color newColor = new Color(c.r,c.g,c.b, newAlpha);
            particleSystem.startColor = newColor;
            yield return null;
        }
    }
}

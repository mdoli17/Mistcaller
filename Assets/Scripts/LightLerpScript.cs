using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;


public class LightLerpScript : MonoBehaviour
{
    Light2D ptLight;
    public float maxIntensity;
    public float minIntensity;
    public float time;
    

    

    // Start is called before the first frame update
    void Start()
    {
        ptLight = GetComponent<Light2D>();
        
        StartCoroutine(countDown());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator countDown()
    {
        float duration = Random.Range(0.0f, 3.0f);
        float totalTime = 0;
        while (totalTime <= duration)
        {
            totalTime += Time.deltaTime;
            
            yield return null;
        }
        StartCoroutine(lowToHigh());
    }

    IEnumerator lowToHigh()
    {
        float counter = 0f;
        while(counter < time)
        {
            if(Time.timeScale == 0)
                counter += Time.unscaledDeltaTime;
            else
                counter += Time.deltaTime;

            ptLight.intensity = Mathf.Lerp(minIntensity,maxIntensity,counter / time);;
            
            yield return null;
        }
        StartCoroutine(highToLow());
    }

    IEnumerator highToLow()
    {
        float counter = 0f;
        while(counter < time)
        {
            if(Time.timeScale == 0)
                counter += Time.unscaledDeltaTime;
            else
                counter += Time.deltaTime;

            ptLight.intensity = Mathf.Lerp(maxIntensity, minIntensity ,counter / time);;
            
            yield return null;
        }
        StartCoroutine(lowToHigh());
    }
}

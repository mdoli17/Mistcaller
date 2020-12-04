using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour
{
    public float lifetime;
    public void doStuff(GameObject obj)
    {
        if(obj == null)
        {
            StartCoroutine(Death(null, 0));
            return;
        }
        Light light = obj.GetComponent<Light>();
        float startInt = 0;
        if(light != null)
        {
            startInt = light.intensity;
            light.intensity = 1f;
        }
        StartCoroutine(Death(light, startInt));
    }

    IEnumerator Death(Light light, float intensity)
    {
        
        yield return new WaitForSeconds(lifetime);
        if(light != null)
        {
            light.intensity = intensity;
        }
        Destroy(this.gameObject);
    }

}

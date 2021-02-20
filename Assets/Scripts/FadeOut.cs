using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer[] FadeRenderers;
    public SpriteRenderer[] DarkSingleRenderers;
    public GameObject[] DarkenFamilyRenderers;

    
    [Range(0,1)]
    public float darkAmmount;    

    public float fadeTime;

    private float[] startAlphas;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        startAlphas = new float[FadeRenderers.Length];
        for(int i = 0; i < FadeRenderers.Length; i++)
        {
            startAlphas[i] = FadeRenderers[i].color.a;
        }   
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
             
            StopAllCoroutines();
            StartCoroutine(DarkenSingle());
            StartCoroutine(DarkenFamily());
            StartCoroutine(FadeIn());
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StopAllCoroutines();
            
            StartCoroutine(LightenSingle());
            StartCoroutine(LightenFamily());
            StartCoroutine(Fadeout());
        }
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        while(t < fadeTime)
        {
            t += Time.deltaTime;
            foreach (var item in FadeRenderers)
            {
                Color color = item.color;
                float alpha = Mathf.Lerp(color.a,0,t / fadeTime);
                item.color = new Color(color.r,color.g,color.b, alpha);
                
            }
            yield return null;
        }
    }

    IEnumerator Fadeout()
    {
        float t = 0f;
        while(t < fadeTime)
        {
            t += Time.deltaTime;

            for(int i = 0; i < FadeRenderers.Length; i++)
            {
                SpriteRenderer item = FadeRenderers[i];
                Color color = item.color;
                float alpha = Mathf.Lerp(color.a,startAlphas[i],t / fadeTime);
                item.color = new Color(color.r,color.g,color.b, alpha);
            }
            // foreach (var item in FadeRenderers)
            // {
            //     Color color = item.color;
            //     float alpha = Mathf.Lerp(color.a,1,t / fadeTime);
            //     item.color = new Color(color.r,color.g,color.b, alpha);
            // }
            yield return null;
        }
    }


    IEnumerator DarkenSingle()
    {
        Debug.Log("Darken");
        float t = 0f;
        while(t < fadeTime)
        {
            t += Time.deltaTime;
            foreach (var item in DarkSingleRenderers)
            {
                Color color = item.color;
                float value =  Mathf.Lerp(color.r, darkAmmount, t / fadeTime);
                item.color = new Color(value, value, value, color.a);
                
            }
            yield return null;
        }
    }

    IEnumerator LightenSingle()
    {
        float t = 0f;
        while(t < fadeTime)
        {
            t += Time.deltaTime;
            foreach (var item in DarkSingleRenderers)
            {
                Color color = item.color;
                float value =  Mathf.Lerp(color.r, 1, t / fadeTime);
                item.color = new Color(value, value, value, color.a);
            }
            yield return null;
        }
    }

    IEnumerator DarkenFamily()
    {
        float t = 0f;
        while (t < fadeTime)
        {
            foreach (var child in DarkenFamilyRenderers)
            {
                SpriteRenderer[] renderers = child.GetComponentsInChildren<SpriteRenderer>();
                t += Time.deltaTime;
                foreach (var item in renderers)
                {
                    Color color = item.color;
                    float value = Mathf.Lerp(color.r, darkAmmount, t / fadeTime);
                    item.color = new Color(value, value, value, color.a);

                }
                yield return null;
            }
        }
    }

    IEnumerator LightenFamily()
    {
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            foreach(var child in DarkenFamilyRenderers)
            {
                SpriteRenderer[] renderers = child.GetComponentsInChildren<SpriteRenderer>();
                foreach (var item in renderers)
                {
                    Color color = item.color;
                    float value = Mathf.Lerp(color.r, 1, t / fadeTime);
                    item.color = new Color(value, value, value, color.a);
                }
            }
            yield return null;
        }
    }
}

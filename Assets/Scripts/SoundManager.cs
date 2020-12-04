using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource source;

    public AudioClip[] BackgroundNoises;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
        foreach(AudioClip clip in BackgroundNoises)
        {
            source.PlayOneShot(clip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

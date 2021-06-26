using System.Collections;
using UnityEngine;

namespace Game
{
    public class SoundHandler : MonoBehaviour
    {
        public AudioSource source;

        public void HandleAudioClip(AudioClip clip)
        {
            source = gameObject.AddComponent<AudioSource>();
            source.PlayOneShot(clip);
            StartCoroutine(HandleFinish(clip.length));
        }

        private IEnumerator HandleFinish(float time)
        {
            yield return new WaitForSeconds(time);
            Debug.Log("Going To Destroy");
            Destroy(this.gameObject);
        }
        
        
    }
}
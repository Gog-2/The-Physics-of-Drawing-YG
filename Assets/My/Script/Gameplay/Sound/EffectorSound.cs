using System.Collections.Generic;
using UnityEngine;

public class EffectorSound : MonoBehaviour
{
    public AudioClip loopSound;
    private AudioSource audioSource;
    private int objectsInside = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.clip = loopSound;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
            objectsInside++;
            if (objectsInside == 1 && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
    }

    void OnTriggerExit2D(Collider2D other)
    {
            objectsInside--;
            
            if (objectsInside <= 0)
            {
                objectsInside = 0;
                audioSource.Stop();
            }
    }
}
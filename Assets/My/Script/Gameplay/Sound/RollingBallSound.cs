using UnityEngine;

public class RollingBallSound : MonoBehaviour
{
    public AudioClip rollingSound;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    
    [Header("Sound Settings")]
    public float minSpeed = 0.5f;
    public float maxSpeed = 10f;
    
    [Header("Pitch Settings")]
    public float minPitch = 0.8f;
    public float maxPitch = 1.5f;
    
    [Header("Volume Settings")]
    public float minVolume = 0.2f;
    public float maxVolume = 0.8f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.clip = rollingSound;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void FixedUpdate()
    {
        float speed = rb.linearVelocity.magnitude;
        
        if (speed > minSpeed)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            float normalizedSpeed = Mathf.Clamp01((speed - minSpeed) / (maxSpeed - minSpeed));
            audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed);
            audioSource.volume = Mathf.Lerp(minVolume, maxVolume, normalizedSpeed);
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}

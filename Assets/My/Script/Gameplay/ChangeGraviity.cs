using UnityEngine;

public class ChangeGraviity : MonoBehaviour
{
    [SerializeField]private AudioSource _changeGravityAudio;
    [SerializeField]private AudioClip _changeGravityClip;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null) rb.gravityScale = -rb.gravityScale;
        _changeGravityAudio.PlayOneShot(_changeGravityClip);
    }
}

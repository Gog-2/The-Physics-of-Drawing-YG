using UnityEngine;

public class ChangeGraviity : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null) rb.gravityScale = -rb.gravityScale;
    }
}

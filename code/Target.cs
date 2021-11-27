
using UnityEngine;

public class Target : MonoBehaviour
{
    public AudioClip DieSound;
    public float health = 50f;


    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        AudioSource.PlayClipAtPoint(DieSound, transform.position); 
        Destroy(gameObject);
        
    }
}

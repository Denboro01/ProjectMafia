using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particles;

    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    Collider2D hitbox;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Invoke("Explode", 0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            Invoke("Explode", 0.2f);
        }
    }

    private void Explode()
    {
        particles.Play();
        sprite.enabled = false;
        hitbox.enabled = true;
        Destroy(gameObject, 1);
    }
}

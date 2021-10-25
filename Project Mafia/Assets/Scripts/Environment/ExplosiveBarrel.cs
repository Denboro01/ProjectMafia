using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particles;
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField] 
    Collider2D hitbox;
    [SerializeField]
    Collider2D c;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            Explode();
        }
    }

    private void Explode()
    {
        particles.Play();
        sprite.enabled = false;
        c.enabled = false;
        hitbox.enabled = true;
        Destroy(gameObject, 1);
    }

}

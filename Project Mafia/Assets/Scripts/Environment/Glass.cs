using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{

    [SerializeField]
    Collider2D c;
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    ParticleSystem particles;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.tag == "PlayerBullet")
        {
            Break();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            Break();
        }
    }

    private void Break()
    {
        c.enabled = false;
        sprite.enabled = false;
        particles.Play();
        Destroy(gameObject, 6);
    }

}

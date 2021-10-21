using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particles;

    [SerializeField]
    SpriteRenderer sprite;

    private Collider2D c;


    private void Start()
    {
        c = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Explode();
        }
    }

    private void Explode()
    {
        particles.Play();
        sprite.enabled = false;
        c.enabled = false;
        Destroy(gameObject, 1);
    }
}

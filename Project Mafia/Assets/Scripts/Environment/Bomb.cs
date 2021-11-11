using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField]
    ParticleSystem particles;

    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    Collider2D hitbox;

    [SerializeField]
    Collider2D c;

    [SerializeField]
    Collider2D pickUpCollider;

    private void Start()
    {
        Invoke("Explode", 3);
    }

    private void Explode()
    {
        particles.Play();
        sprite.enabled = false;
        c.enabled = false;
        pickUpCollider.enabled = false;
        hitbox.enabled = true;
        Destroy(gameObject, 1);
    }
}

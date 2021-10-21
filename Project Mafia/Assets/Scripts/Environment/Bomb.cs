using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public ParticleSystem particles;

    [SerializeField]
    SpriteRenderer sprite;

    private void Start()
    {

        Invoke("Explode", 3);
    }

    private void Explode()
    {
        Debug.Log("Kaboom");
        particles.Play();
        sprite.enabled = false;
        Destroy(gameObject, 1);
    }
}

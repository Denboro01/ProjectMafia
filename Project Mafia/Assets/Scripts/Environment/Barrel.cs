using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Barrel : MonoBehaviour
{
    public GameObject bomb; 

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            Destroyed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            Destroyed();
        }
    }

    private void Destroyed()
    {
        if (Random.Range(0, 3) == 2)
        {
            Instantiate(bomb, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }


}

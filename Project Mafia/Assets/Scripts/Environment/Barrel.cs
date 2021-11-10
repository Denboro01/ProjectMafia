using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Barrel : MonoBehaviour
{
    public GameObject bomb;
    public GameObject item;
    public GameObject gun;
    public int randomNumber;

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

    public void Destroyed()
    {
        randomNumber = Random.Range(0, 3);
        if (randomNumber == 2)
        {
            Instantiate(bomb, transform.position, transform.rotation);
        } else if (randomNumber == 1)
        {
            Instantiate(item, transform.position, transform.rotation);
        } else
        {
            Instantiate(gun, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }


}

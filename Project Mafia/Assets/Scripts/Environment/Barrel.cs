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
            if (Random.Range(0,3) == 2)
            {
                Instantiate(bomb, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    
}

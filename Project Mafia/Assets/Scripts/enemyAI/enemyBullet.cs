using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float enemyBulletSpeed = 20f;

    public Rigidbody2D rb;

    private EnemiesAI enemyAI;


    private void Start()
    {
        enemyAI = GetComponent<EnemiesAI>();
    }

    private void Update()
    {
        rb.AddForce(transform.right * enemyBulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
}
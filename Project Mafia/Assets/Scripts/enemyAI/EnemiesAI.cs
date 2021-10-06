using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAI : MonoBehaviour
{
    public GameObject player;
    public Transform enemyFirePoint;
    public Vector3 lastPlayerPosition;
    public playerDetector playerDetector;
    public bool startPathing;

    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private LayerMask wallLayerMask;

    private bool fireCooldown;
    private float timer;
    private float forgetPlayerTimer;
    private bool playerSeen;
    private Rigidbody2D rb;

    void Start()
    {
        timer = 2;
        wallLayerMask = LayerMask.GetMask("Default");

        enemyMovement = GetComponent<EnemyMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (playerDetector.playerInRange)
        {
            visibilityCheck();
        }

        Timer();

        if (playerSeen && forgetPlayerTimer <= 1)
        {
            lastPlayerPosition = player.transform.position;
            startPathing = true;
        }
    }


    private void visibilityCheck()
    {


        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, wallLayerMask);
        if (hit.collider == null)
        {

            playerSeen = true;
            startPathing = false;
            forgetPlayerTimer = 4;
            
            enemyMovement.isChasing = true;

            rb.velocity = Vector3.zero;

            Firing();

        }

    }

    private void Timer()
    {
        if (playerSeen)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                fireCooldown = false;
            }
            if (timer <= 0)
            {
                fireCooldown = true;
                timer = 1;
            }
        }


        if (forgetPlayerTimer > 0)
        {
            forgetPlayerTimer -= Time.deltaTime;
        }
        else
        {
            playerSeen = false;
        }
    }

    private void Firing()
    {
        if (fireCooldown)
        {
            Instantiate(enemyBullet, enemyFirePoint.position, enemyFirePoint.rotation);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAI : MonoBehaviour
{
    public int enemyHealth;
    public int enemyMaxHealth;

    public GameObject player;
    public Transform enemyFirePoint;
    public Vector3 lastPlayerPosition;
    public playerDetector playerDetector;
    public bool startPathing;
    public bool playerSeen;

    [SerializeField] private enemyMovement enemyMovement;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private LayerMask wallLayerMask;

    private bool fireCooldown;
    private float timer;
    private float forgetPlayerTimer;

    void Start()
    {
        enemyHealth = enemyMaxHealth;
        timer = 2;

        enemyMovement = GetComponent<enemyMovement>();
    }

    void FixedUpdate()
    {
        if (playerDetector.playerInRange)
        {
            VisibilityCheck();
        }

        Timer();

        if (playerSeen && forgetPlayerTimer <= 1)
        {
            lastPlayerPosition = player.transform.position;
            startPathing = true;
            playerSeen = false;
        }
    }

    private void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void VisibilityCheck()
    {

        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, wallLayerMask);
        if (hit.collider == null)
        {
            playerSeen = true;
            startPathing = false;
            forgetPlayerTimer = 2;
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

        Vector3 firingDirection = new Vector3(player.transform.position.x, player.transform.position.y);
        Vector3 dir = firingDirection - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (fireCooldown)
        {
            Instantiate(enemyBullet, enemyFirePoint.position, rotation);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PunchPoint")
        {
            if (!playerDetector.playerInRange)
            {
                Destroy(gameObject);
            } else
            {
                enemyHealth -= 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            enemyHealth -= 3;
            Destroy(collision.gameObject);
        }
    }


}

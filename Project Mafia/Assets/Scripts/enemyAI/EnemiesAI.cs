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
    public bool playerSeen;

    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private LayerMask wallLayerMask;

    private bool fireCooldown;
    private float timer;
    private float forgetPlayerTimer;

    void Start()
    {
        timer = 2;
        wallLayerMask = LayerMask.GetMask("Obstacles");

        enemyMovement = GetComponent<EnemyMovement>();
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


}

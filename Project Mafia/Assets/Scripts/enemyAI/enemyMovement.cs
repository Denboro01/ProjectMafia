using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyMovement : MonoBehaviour
{

    public Transform target;
    public Path path;

    public bool willLookAround;
    public bool isChasing;

    [SerializeField] private EnemiesAI enemyAI;
    [SerializeField] private float enemySpeed = 200f;
    [SerializeField] private float nextWaypointDistance = 0.5f;

    [SerializeField]
    private SpriteRenderer spriteLeft;
    [SerializeField]
    private SpriteRenderer spriteRight;
    [SerializeField]
    private SpriteRenderer spriteUp;
    [SerializeField]
    private SpriteRenderer spriteBottom;

    [SerializeField]
    private GameObject playerDetector;


    private int currentWaypoint;
    private Vector3 lastPosition;
    private float rotateTimer;
    private bool Turn;
    private float turnCounter;

    private bool rotationHasReset;
    public Vector2 direction;
    private Vector2 rotation;
    private Vector2 force;
    private Seeker seeker;
    private Rigidbody2D rb;

    private void Start()
    {

        rotateTimer = 4;
        isChasing = false;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemiesAI>();

    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }



    private void FixedUpdate()
    {


        if (willLookAround)
        {
            if (!isChasing)
            {
                LookAround();
                if (!rotationHasReset)
                {
                    playerDetector.transform.rotation = Quaternion.Euler(0, 0, 0);
                    rotationHasReset = true;
                }

            }
        }

        if (enemyAI.startPathing)
        {
            isChasing = true;
            enemyAI.startPathing = false;
            rotationHasReset = false;
            lastPosition = enemyAI.lastPlayerPosition;
            seeker.StartPath(rb.position, lastPosition, OnPathComplete);
        }

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        if (enemyAI.playerSeen)
        {
            path = null;
        }

        if (path != null)
        {
            direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position);
            rotation = direction;
            force = enemySpeed * Time.deltaTime * direction;

            LookForPlayer();

            rb.velocity = force;


            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                isChasing = false;
                enemyAI.startPathing = false;
            }
        }

    }


    private void LookForPlayer()
    {

        // if it works...(yes i know this code sucks, but i was tired and running low on time)
        if (rotation.x > 0 && rotation.x > rotation.y)
        {
            playerDetector.transform.rotation = Quaternion.Euler(0, 0, 0);
            spriteBottom.enabled = false;
            spriteLeft.enabled = false;
            spriteRight.enabled = true;
            spriteUp.enabled = false;
        }
        if (rotation.y > 0 && rotation.y > rotation.x)
        {
            playerDetector.transform.rotation = Quaternion.Euler(0, 0, 90);
            spriteBottom.enabled = false;
            spriteLeft.enabled = false;
            spriteRight.enabled = false;
            spriteUp.enabled = true;
        }
        if (rotation.x < 0 && -rotation.x > rotation.y)
        {
            playerDetector.transform.rotation = Quaternion.Euler(0, 0, 180);
            spriteBottom.enabled = false;
            spriteLeft.enabled = true;
            spriteRight.enabled = false;
            spriteUp.enabled = false;
        }
        if (rotation.y < 0 && -rotation.y > rotation.x)
        {
            playerDetector.transform.rotation = Quaternion.Euler(0, 0, 270);
            spriteBottom.enabled = true;
            spriteLeft.enabled = false;
            spriteRight.enabled = false;
            spriteUp.enabled = false;
        }


    }
    private void Update()
    {
        if (Turn)
        {
            if (turnCounter == 0)
            {
                playerDetector.transform.rotation = Quaternion.Euler(0, 0, 0);
                spriteBottom.enabled = false;
                spriteLeft.enabled = false;
                spriteRight.enabled = true;
                spriteUp.enabled = false;
            }
            if (turnCounter == 1)
            {
                playerDetector.transform.rotation = Quaternion.Euler(0, 0, 270);
                spriteBottom.enabled = true;
                spriteLeft.enabled = false;
                spriteRight.enabled = false;
                spriteUp.enabled = false;
            }
            if (turnCounter == 2)
            {
                playerDetector.transform.rotation = Quaternion.Euler(0, 0, 90);
                spriteBottom.enabled = false;
                spriteLeft.enabled = false;
                spriteRight.enabled = false;
                spriteUp.enabled = true;
            }
            if (turnCounter == 3)
            {
                playerDetector.transform.rotation = Quaternion.Euler(0, 0, 180);
                spriteBottom.enabled = false;
                spriteLeft.enabled = true;
                spriteRight.enabled = false;
                spriteUp.enabled = false;
            }
        }
    }

    private void LookAround()
    {
        if (rotateTimer > 0)
        {
            rotateTimer -= Time.deltaTime;
            Turn = false;
        }
        if (rotateTimer <= 0)
        {
            rotateTimer = 4;
            Turn = true;
        }


        if (Turn)
        {
            turnCounter++;
            if (turnCounter == 4)
            {
                turnCounter = 0;
            }
        }
    }
}

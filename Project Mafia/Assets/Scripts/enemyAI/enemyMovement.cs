using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{

    public Transform target;
    public Path path;

    public bool willLookAround;
    public bool isChasing;

    [SerializeField] private EnemiesAI enemyAI;
    [SerializeField] private float enemySpeed = 200f;
    [SerializeField] private float nextWaypointDistance = 0.5f;

    private int currentWaypoint;
    private Vector3 lastPosition;
    private float rotateTimer;
    private bool Turn;
    
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
                    transform.rotation = Quaternion.Euler(0, 0, 0);
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
        /*
        Vector3 tempDirection = new Vector3(force.x * enemySpeed, force.y * enemySpeed);
        Vector3 dir = tempDirection - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        */

        // if it works...
        if (rotation.x > 0 && rotation.x > rotation.y)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } 
        if (rotation.y > 0 && rotation.y > rotation.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        } 
        if (rotation.x < 0 && -rotation.x > rotation.y)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        } 
        if (rotation.y < 0 && -rotation.y > rotation.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
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
            transform.Rotate(0,0,90f);
        }
    }
}

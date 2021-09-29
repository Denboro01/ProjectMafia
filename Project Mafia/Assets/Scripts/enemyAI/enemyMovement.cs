using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyMovement : MonoBehaviour
{

    public Transform target;

    [SerializeField] private enemyAI enemyAI;
    [SerializeField] private float enemySpeed = 400f;
    [SerializeField] private float nextWaypointDistance = 0.5f;

    private int currentWaypoint;
    private Vector3 lastPosition;
    private float rotateTimer;
    private bool Turn;

    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;

    private void Start()
    {
        rotateTimer = 4;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<enemyAI>();
        
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        LookAround();

        if (enemyAI.startPathing)
        {
            enemyAI.startPathing = false;
            lastPosition = enemyAI.lastPlayerPosition;
            seeker.StartPath(rb.position, lastPosition, OnPathComplete);
        }
    }

    private void FixedUpdate()
    {
        if (path == null) {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position);
        Vector2 force = direction * enemySpeed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }


    private void LookAround()
    {
        if (rotateTimer > 0)
        {
            rotateTimer -= Time.deltaTime;
        }
        if (rotateTimer <= 0)
        {
            rotateTimer = 4;
            Turn = true;
        }

        if (Turn)
        {
            //transform.Rotate(0, 0, 90f, Space.Self);
        }
    }
}

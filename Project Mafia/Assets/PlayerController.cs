using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int health = 100;

    #region Initialize State Machine
    public enum PlayerState
    {
        idle,
        move,
        attack,
        hurt,
        death
    }

    public PlayerState state;
    #endregion

    private Vector2 movement;
    [SerializeField]
    private float movementSpeed = 200f;

    private Rigidbody2D rb;

    public int currentAmmo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontalInput, verticalInput).normalized;

        switch (state)
        {
            case PlayerState.idle:
                // Manage states
                if (horizontalInput != 0 || verticalInput != 0)
                {
                    state = PlayerState.move;
                } else if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = PlayerState.attack;
                }
                break;

            case PlayerState.move:
                // Manage states
                if (horizontalInput == 0 && verticalInput == 0)
                {
                    state = PlayerState.idle;
                } else if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = PlayerState.attack;
                }
                break;

            case PlayerState.attack:
                if (currentAmmo <= 0)
                {
                    // Shoots
                } else
                {
                    // Combat
                }
                break;

            case PlayerState.hurt:

                break;

            case PlayerState.death:
                Destroy(gameObject);
                break;
        }

        Debug.Log(state);
        
        // Check if player should be dead

        if (health <= 0 && state != PlayerState.death)
        {
            state = PlayerState.death;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.velocity = movement * movementSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && state == PlayerState.attack)
        {
            Vector3 knockBack = (collision.transform.position - transform.position).normalized;

            collision.transform.position += knockBack;
        }
    }
}

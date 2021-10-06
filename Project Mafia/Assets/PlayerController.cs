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
        fight,
        shoot,
        hurt,
        death
    }

    public PlayerState state;
    #endregion

    private float horizontalInput;
    private float verticalInput;

    private Vector2 movement;
    [SerializeField]
    private float movementSpeed = 200f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontalInput, verticalInput).normalized;

        switch (state)
        {
            case PlayerState.idle:

                break;

            case PlayerState.move:

                break;

            case PlayerState.fight:

                break;

            case PlayerState.shoot:

                break;

            case PlayerState.hurt:

                break;

            case PlayerState.death:
                Destroy(gameObject);
                break;
        }
     
        
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
}

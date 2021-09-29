using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Initialize variables for player input
    private float horizontalInput;
    private float verticalInput;

    [SerializeField]
    private float movementSpeed = 275f;

    public Rigidbody2D rb;

    private Vector2 movement;

    private float finalAngle;

    // Update is called once per frame
    void Update()
    {
        // Updates the variables when player gives input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontalInput, verticalInput).normalized;
        if (horizontalInput != 0 || verticalInput != 0)
        {
            Rotate();
        }

        rb.rotation = finalAngle;

    }

    private void FixedUpdate()
    {
        // Applies movement
        rb.velocity = movement * movementSpeed * Time.fixedDeltaTime;
    }

    private void Rotate()
    {
        float lookAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        if (lookAngle == 45 || lookAngle == -45)
        {
            lookAngle = 0;
        }

        if (lookAngle == 135 || lookAngle == -135)
        {
            lookAngle = 180;
        }

        finalAngle = lookAngle;

        rb.rotation = lookAngle;
    }
}

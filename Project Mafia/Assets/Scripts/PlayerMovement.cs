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

    // Update is called once per frame
    void Update()
    {
        // Updates the variables when player gives input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontalInput, verticalInput).normalized;

        Rotate();
    }

    private void FixedUpdate()
    {
        // Applies movement
        rb.velocity = movement * movementSpeed * Time.fixedDeltaTime;
    }

    private void Rotate()
    {
        float lookAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        rb.rotation = lookAngle;
    }
}

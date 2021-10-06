using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int health = 100;
    public int currentAmmo;
    private float currentFireRate;
    private float weaponFireRate;
    private Vector2 movement;
    private float movementSpeed = 200f;

    private Vector3 bulletSpawnOffset;

    private float lastX;
    private float lastY;

    private Rigidbody2D rb;

    public GameObject bulletPrefab;

    public enum PlayerState
    {
        idle,
        move,
        attack,
        hurt,
        death
    }

    public PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Initialize player input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        movement = new Vector2(horizontalInput, verticalInput).normalized;

        float bulletAngle = Mathf.Atan2(lastY, lastX) * Mathf.Rad2Deg;

        if (bulletAngle == 45 || bulletAngle == -45)
        {
            bulletAngle = 0;
            lastY = 0;
        } else if (bulletAngle == 135 || bulletAngle == -135)
        {
            bulletAngle = 180;
            lastY = 0;
        }

        float bulletPositionX = 1 * lastX;
        float bulletPositionY = 1 * lastY;

        bulletSpawnOffset = new Vector3(bulletPositionX, bulletPositionY);

        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }

        #region State Machine
        switch (state)
        {
            #region Idle State
            case PlayerState.idle:
                // Play idle animation

                // Manage state
                if (horizontalInput != 0 || verticalInput != 0)
                {
                    state = PlayerState.move;
                } else if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = PlayerState.attack;
                }
                break;
            #endregion

            #region Move State
            case PlayerState.move:
                // Play movement animation

                // Manage state
                if (horizontalInput == 0 && verticalInput == 0)
                {
                    state = PlayerState.idle;
                } else if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = PlayerState.attack;
                } else
                {
                    lastX = horizontalInput;
                    lastY = verticalInput;
                }
                break;
            #endregion

            #region Attack State
            case PlayerState.attack:
                if (currentAmmo > 0 && currentFireRate <= 0)
                {
                    // Fire animation

                    // Fire
                    Instantiate(bulletPrefab, transform.position + bulletSpawnOffset, Quaternion.Euler(new Vector3 (0, 0, bulletAngle)));

                    currentAmmo--;
                    currentFireRate = weaponFireRate;

                    // Manage state
                    state = PlayerState.idle;
                } else if (currentAmmo <= 0)
                {
                    // Punch animation

                    // punch

                    // Manage state
                    state = PlayerState.idle;
                } else
                {
                    state = PlayerState.idle;
                }
                break;
            #endregion

            #region Hurt State
            case PlayerState.hurt:
                // Play hurt animation

                // Manage state
                state = PlayerState.idle;
                break;
            #endregion

            #region Death State
            case PlayerState.death:
                Destroy(gameObject);
                break;
            #endregion
        }
        #endregion

        if (health <= 0 && state != PlayerState.death)
        {
            state = PlayerState.death;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * movementSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item" && Input.GetKey(KeyCode.E)) {
            currentAmmo = collision.GetComponent<WeaponStats>().weaponAmmo;
            weaponFireRate = collision.GetComponent<WeaponStats>().fireRate;
            Destroy(collision.gameObject);
        }
    }
}

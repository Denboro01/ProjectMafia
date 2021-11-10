using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int health = 100;
    public int currentAmmo;
    private float bombCount;
    private float currentFireRate;
    private float weaponFireRate;
    private Vector2 movement;
    private float movementSpeed = 200f;
    private float lastX;
    private float lastY;

    private Rigidbody2D rb;

    private Vector3 bulletSpawnOffset;
    public GameObject bulletPrefab;
    public GameObject bomb;

    public Transform punchPoint;
    public float punchRange = 0.75f;
    public LayerMask enemyLayers;
    public float ventCooldown;

    public Animator anim;

    public enum PlayerState
    {
        idle,
        move,
        attack,
        bomb,
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
        Debug.Log(ventCooldown);
        VentTimer();
        // Initialize player input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontalInput, verticalInput).normalized;

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
                anim.SetFloat("lastX", lastX);
                anim.SetFloat("lastY", lastY);
                anim.SetFloat("speed", movement.sqrMagnitude);

                // Manage state
                if (horizontalInput != 0 || verticalInput != 0)
                {
                    state = PlayerState.move;
                } else if (Input.GetKey(KeyCode.Space))
                {
                    state = PlayerState.attack;
                } else if (Input.GetKeyDown(KeyCode.B) && bombCount > 0)
                {
                    state = PlayerState.bomb;
                }
                break;
            #endregion

            #region Move State
            case PlayerState.move:
                // Play movement animation
                anim.SetFloat("horizontal", horizontalInput);
                anim.SetFloat("vertical", verticalInput);
                anim.SetFloat("speed", movement.sqrMagnitude);

                // Manage state
                if (horizontalInput == 0 && verticalInput == 0)
                {
                    state = PlayerState.idle;
                } else if (Input.GetKey(KeyCode.Space))
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
                    float bulletAngle = Mathf.Atan2(lastY, lastX) * Mathf.Rad2Deg;

                    if (bulletAngle == 45 || bulletAngle == -45)
                    {
                        bulletAngle = 0;
                        lastY = 0;
                    }
                    else if (bulletAngle == 135 || bulletAngle == -135)
                    {
                        bulletAngle = 180;
                        lastY = 0;
                    }

                    float bulletPositionX = 1.5f * lastX;
                    float bulletPositionY = 1.5f * lastY;

                    bulletSpawnOffset = new Vector3(bulletPositionX, bulletPositionY);

                    Instantiate(bulletPrefab, transform.position + bulletSpawnOffset, Quaternion.Euler(new Vector3 (0, 0, bulletAngle)));

                    currentAmmo--;
                    currentFireRate = weaponFireRate;

                    // Manage state
                    state = PlayerState.idle;
                } else if (currentAmmo <= 0)
                {
                    // Punch animation

                    // punch

                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPoint.position, punchRange, enemyLayers);

                    foreach (Collider2D enemy in hitEnemies)
                    {
                        Vector3 knockBack = (enemy.transform.position - punchPoint.position).normalized;

                        enemy.transform.position += knockBack;
                    }

                    // Manage state
                    state = PlayerState.idle;
                } else
                {
                    state = PlayerState.idle;
                }
                break;
            #endregion

            #region bomb State
            case PlayerState.bomb:
                float bombPositionX = 1.5f * lastX;
                float bombPositionY = 1.5f * lastY;

                bulletSpawnOffset = new Vector3(bombPositionX, bombPositionY);
                Instantiate(bomb, transform.position + bulletSpawnOffset, transform.rotation);
                
                bombCount--;
                state = PlayerState.idle;
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

        if (currentAmmo > 0)
        {
            anim.SetBool("gotGun", true);
        } else
        {
            anim.SetBool("gotGun", false);
        }

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
        /*
        if (collision.gameObject.tag == "Weapon" && Input.GetKey(KeyCode.E)) {
            currentAmmo = collision.GetComponent<WeaponStats>().weaponAmmo;
            weaponFireRate = collision.GetComponent<WeaponStats>().fireRate;
            Destroy(collision.gameObject);
        }
        */
        if (Input.GetKey(KeyCode.E))
        {
            if (collision.gameObject.tag == "Weapon")
            {
                currentAmmo = collision.GetComponent<WeaponStats>().weaponAmmo;
                weaponFireRate = collision.GetComponent<WeaponStats>().fireRate;
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "Bomb")
            {
                bombCount++;
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ëxplosion")
        {
            // take DMG
            state = PlayerState.hurt;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(punchPoint.position, punchRange);
    }

    private void VentTimer()
    {
        if (ventCooldown > 0) 
        {
            ventCooldown -= Time.deltaTime;
        }
        if (ventCooldown <= 0)
        {
            ventCooldown = 0;
        }
    }
}

using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public int currentAmmo;
    private float bombCount;
    private float currentFireRate;
    private float weaponFireRate;
    private Vector2 movement;
    private float movementSpeed = 200f;
    private float lastX;
    private float lastY;
    private float iFrameTimer;
    private float punchTimer;

    private Rigidbody2D rb;

    private Vector3 bulletSpawnOffset;
    public GameObject bulletPrefab;
    public GameObject bomb;
    public Collider2D punchPointCollider;

    public Transform punchPoint;
    public float punchRange = 0.75f;
    public LayerMask enemyLayers;
    public float ventCooldown;
    public float punchMultiplier;

    public HealthBar healthBar;

    public static Action<int> InitializePlayer;
    public static Action<int> PewPew;
    public static Action<int> PlayerHealth;

    public enum PlayerState
    {
        idle,
        move,
        shoot,
        punch,
        bomb,
        hurt,
        death
    }

    public PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        health = maxHealth;

        healthBar.SetMaxHealth(maxHealth);
        InitializePlayer?.Invoke(currentAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        Timers();
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
                punchPointCollider.enabled = false;
                // Manage state
                if (horizontalInput != 0 || verticalInput != 0)
                {
                    state = PlayerState.move;
                }
                else if (Input.GetButton("Fire1"))
                {
                    state = PlayerState.shoot;
                }
                else if (Input.GetButtonDown("Fire2")) 
                {
                    state = PlayerState.punch;
                }
                else if (Input.GetKeyDown(KeyCode.B) && bombCount > 0)
                {
                    state = PlayerState.bomb;
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
                }
                else if (Input.GetButton("Fire1"))
                {
                    state = PlayerState.shoot;
                }
                else if (Input.GetButton("Fire2"))
                {
                    state = PlayerState.punch;
                }
                else
                {
                    lastX = horizontalInput;
                    lastY = verticalInput;
                }
                break;
            #endregion

            #region Shoot State
            case PlayerState.shoot:
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
                    PewPew?.Invoke(currentAmmo);

                    currentFireRate = weaponFireRate;

                    // Manage state
                    state = PlayerState.idle;
                } else
                {
                    state = PlayerState.idle;
                }
                break;
            #endregion

            #region Punch State
            case PlayerState.punch:
                // Punch animation

                // punch

                if (punchTimer <= 0)
                {
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(punchPoint.position, punchRange, enemyLayers);
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        punchPointCollider.enabled = true;
                        if (enemy.attachedRigidbody != null)
                        {
                            Vector2 knockBack = (enemy.transform.position - punchPoint.position).normalized;
                            enemy.attachedRigidbody.AddForce(knockBack * punchMultiplier);
                        }
                    }
                    punchTimer = 0.5f;
                }
                state = PlayerState.idle;
                // Manage state
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


                PlayerHealth?.Invoke(health);

                // Manage state
                state = PlayerState.idle;
                break;
            #endregion

            #region Death State
            case PlayerState.death:
                Debug.Log("U ded. RIP");
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
        if (iFrameTimer <= 0)
        {
            if (collision.gameObject.tag == "Explosion")
            {
                iFrameTimer = 0.1f;
                health -= 50;
                state = PlayerState.hurt;
            }
            if (collision.gameObject.tag == "EnemyProjectile")
            {
                iFrameTimer = 0.1f;
                health -= 20;
                state = PlayerState.hurt;
            }
        }
        
    }

    
    private void Timers()
    {
        if (ventCooldown > 0) 
        {
            ventCooldown -= Time.deltaTime;
        }
        if (ventCooldown <= 0)
        {
            ventCooldown = 0;
        }

        if (iFrameTimer > 0)
        {
            iFrameTimer -= Time.deltaTime;
        }
        if (iFrameTimer <= 0)
        {
            iFrameTimer = 0;
        }

        if (punchTimer > 0)
        {
            punchTimer -= Time.deltaTime;
        }
        if (punchTimer <= 0)
        {
            punchTimer = 0;
        }
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(punchPoint.position, punchRange);
    }
    */

}

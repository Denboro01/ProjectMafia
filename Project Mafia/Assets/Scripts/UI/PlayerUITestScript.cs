using UnityEngine;
using System;

public class PlayerUITestScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    private int equipAmmo = 10;
    public int currentAmmo;

    public HealthBar healthBar;

    public static Action<int> InitializePlayer;
    public static Action<int> PewPew;

    public static Action<int> PlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = equipAmmo;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        InitializePlayer?.Invoke(currentAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);

            if (currentAmmo > 0)
            {
                currentAmmo--;

                PewPew?.Invoke(currentAmmo);
            }
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        PlayerHealth?.Invoke(health);
    }
}

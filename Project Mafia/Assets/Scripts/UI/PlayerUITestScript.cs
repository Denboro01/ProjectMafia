using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUITestScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        healthBar.SetHealth(health);
    }
}

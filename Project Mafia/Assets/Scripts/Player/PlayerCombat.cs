using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public GameObject attackPoint;

    public float attackRange = 0.5f;

    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && attackPoint.activeSelf == true)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Detect enemies that are in range and apply damage
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Rigidbody2D enemyRB = enemy.GetComponent<Rigidbody2D>();
            Vector2 difference = enemy.transform.position - this.transform.position;
            enemyRB.AddForce(difference * 2f, ForceMode2D.Impulse);

            Debug.Log("We hit " + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
}

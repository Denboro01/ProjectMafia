using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPoint : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput == 1 && verticalInput == 0)
        {
            offset = new Vector3(.5f, 0, 0);
        }  else if (horizontalInput == 0 && verticalInput == 1)
        {
            offset = new Vector3(0, .5f, 0);
        } else if (horizontalInput == -1 && verticalInput == 0)
        {
            offset = new Vector3(-.5f, 0, 0);
        } else if (horizontalInput == 0 && verticalInput == -1)
        {
            offset = new Vector3(0, -.5f, 0);
        }

        transform.position = player.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 knockBack = (collision.gameObject.transform.position - transform.position).normalized;

            collision.gameObject.transform.position += knockBack;
        }
    }
}

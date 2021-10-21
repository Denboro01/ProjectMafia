using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassShards : MonoBehaviour
{

    private float timer = 2f;

    private void FixedUpdate()
    {
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        } else if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 0)
        {
            Destroy(gameObject);
        }
    }

}

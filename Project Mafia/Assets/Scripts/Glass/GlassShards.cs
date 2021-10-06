using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassShards : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 0)
        {
            Destroy(gameObject);
        }
    }

}

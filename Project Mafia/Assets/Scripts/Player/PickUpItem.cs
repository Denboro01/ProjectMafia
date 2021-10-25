using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public bool hasGun = false;
    public float bombCount;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Item" && Input.GetKey(KeyCode.G))
        {
            hasGun = true;
            Destroy(collision.gameObject);
        }
    }
}

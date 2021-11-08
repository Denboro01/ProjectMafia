using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    [SerializeField]
    private GameObject otherVent;

    private Vector3 otherVentLocation;
    public PlayerController playercontroller;

    private void Start()
    {
        otherVentLocation = otherVent.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) && playercontroller.ventCooldown == 0)
        {
            playercontroller.ventCooldown = 5f;
            collision.gameObject.transform.position = otherVentLocation;
        } 
    }
}

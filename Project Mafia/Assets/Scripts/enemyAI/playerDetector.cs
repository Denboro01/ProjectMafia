using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour
{
    private float Timer;

    public bool playerInRange;
    public GameObject enemyToDetectFor;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        } else
        {
            playerInRange = false;

        }
    }


}

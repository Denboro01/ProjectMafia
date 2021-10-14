using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPoint : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    [SerializeField]
    public float offsetValue = 0.75f;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput == 1 && (verticalInput == 0 || verticalInput == 1 || verticalInput == -1))
        {
            offset = new Vector3(offsetValue, 0, 0);
        }  else if (horizontalInput == 0 && verticalInput == 1)
        {
            offset = new Vector3(0, offsetValue, 0);
        } else if (horizontalInput == -1 && (verticalInput == 0 || verticalInput == 1 || verticalInput == -1))
        {
            offset = new Vector3(-offsetValue, 0, 0);
        } else if (horizontalInput == 0 && verticalInput == -1)
        {
            offset = new Vector3(0, -offsetValue, 0);
        }

        transform.position = player.position + offset;
    }
}

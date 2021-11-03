using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Transform Player;

    private float offset = -10f;
    private void LateUpdate()
    {
        transform.position = new Vector3 (Player.transform.position.x, Player.transform.position.y, offset);
    }
}

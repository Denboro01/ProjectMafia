using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Barrel : MonoBehaviour
{
    private GraphUpdateScene guo;


    private void Start()
    {
        guo = GetComponent<GraphUpdateScene>();
    }

    private void FixedUpdate()
    {
        AstarPath.active.Scan();
        guo.Apply();
    }
}

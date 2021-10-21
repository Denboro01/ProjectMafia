using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerPlaceholder : MonoBehaviour
{
    
    private void FixedUpdate()
    {
        AstarPath.active.Scan();
    }
}

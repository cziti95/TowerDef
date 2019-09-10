using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static List<Transform> waypoints;

    private void Awake()
    {
        waypoints = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
            waypoints.Add(transform.GetChild(i));
    }
}

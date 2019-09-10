using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int healt;
    public GameObject splashEffect;

    private float distance = 0.5f;
    private Transform target;
    private int waypointIndex = 1;

    public float turnSpeed = 10f;

    private void Start()
    {
        target = Waypoints.waypoints[waypointIndex];
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;

        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (Vector3.Distance(transform.position, target.position) <= distance)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Count - 1)
        {
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    public void TakeDamage(int dmgAmount)
    {
        healt -= dmgAmount;

        if (healt <= 0)
        {
            InitDeath();
        }
    }

    private void InitDeath()
    {
        GameObject effect = Instantiate(splashEffect, transform.position, transform.rotation) as GameObject;
        Destroy(effect, 2f);

        Destroy(gameObject);

        UiManager.instance.IncrementPoints();
    }
}

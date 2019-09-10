using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private Vector3 targetPosition;

    public float bulletSpeed = 30f;
    public int bulletDamage = 50;

    public void Chase(Transform obj)
    {
        target = obj;
        targetPosition = new Vector3(obj.position.x, obj.position.y, obj.position.z);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = targetPosition - transform.position;
        targetPosition += direction;

        float distance = bulletSpeed * Time.deltaTime;
        transform.Translate(direction.normalized * distance, Space.World);

        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            enemy.TakeDamage(bulletDamage);
        }

        Destroy(gameObject);
    }
}

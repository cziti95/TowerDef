using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, 3f);
    }

    public void SpawnEnemy()
    {
        StartCoroutine("InitEnemy");
    }

    IEnumerator InitEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;

        yield return null;
    }

}

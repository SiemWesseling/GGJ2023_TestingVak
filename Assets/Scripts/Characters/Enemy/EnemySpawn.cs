using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float distanceFromPlayer;

    [SerializeField] private GameObject[] Enemy;

    [SerializeField] private EnemyWavesSpawner enemyWavesSpawner;
 
    private GameObject SpawnedEnemy;

    public void SpawnEnemy()
    {
        SpawnedEnemy = Enemy[Random.Range(0, Enemy.Length)];
        Vector2 Distance = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * distanceFromPlayer;
        Vector2 SpawnPosition = GameManager.Instance.player.transform.position;
        SpawnPosition += Distance;
        Instantiate(SpawnedEnemy, SpawnPosition, Quaternion.identity, enemyWavesSpawner.transform.parent);
    }
}

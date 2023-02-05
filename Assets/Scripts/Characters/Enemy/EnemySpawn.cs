using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float distanceFromPlayer;

    [SerializeField] private GameObject[] Enemy;

    [SerializeField] private EnemyWavesSpawner enemyWavesSpawner;
 
    private GameObject SpawnedEnemy;

    private float maxRange;

    private void Start()
    {
        maxRange = distanceFromPlayer + 10;
    }

    public void SpawnEnemy()
    {
        SpawnedEnemy = Enemy[Random.Range(0, Enemy.Length)];
        Vector2 Distance = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * distanceFromPlayer;
        Vector2 SpawnPosition = GameManager.Instance.player.transform.position;
        Debug.Log(Distance);
        SpawnPosition += Distance;
        Instantiate(SpawnedEnemy, SpawnPosition, Quaternion.identity, enemyWavesSpawner.transform);
    }
}

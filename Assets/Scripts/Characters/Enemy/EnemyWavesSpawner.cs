using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesSpawner : MonoBehaviour
{
    //Todo: every enemy spawns as a child of enemy parent to be able to count enemies
    //Todo: if x amount of children left, start timer2
    //Todo: if children left smaller than x or timer <= 0, then start spawning next wave of enemies based on timer1

    [SerializeField] private int amountOfEnemiesPerWave;

    [SerializeField] private GameObject EnemySpawner;

    [Range(0, 100)]
    [SerializeField] private float waveTimer;
    private float waveTimeSeconds;

    [Range(0, 100)]
    [SerializeField] private int amountOfEnemiesLeftBeforeNewSpawn;

    [Range(0, 1)]
    [SerializeField] private float spawnTimer;

    private EnemySpawn enemySpawn;

    private bool coRoutineHasStarted;

    private void Start()
    {
        enemySpawn = GetComponent<EnemySpawn>();

        waveTimeSeconds = waveTimer;

        StartCoroutine(SpawnEnemies());
    }

    public void Update()
    {
        if (waveTimer >= 0)
        {
            if (EnemySpawner.transform.childCount > amountOfEnemiesLeftBeforeNewSpawn)
            {
                waveTimer -= Time.deltaTime;
            }
            if (waveTimer < 0 || EnemySpawner.transform.childCount <= amountOfEnemiesLeftBeforeNewSpawn)
            {
                if (!coRoutineHasStarted)
                {
                    StartCoroutine(SpawnEnemies());
                }

                waveTimer = waveTimeSeconds;
            }
        }
    }

    private IEnumerator SpawnEnemies()
    {
        coRoutineHasStarted = true;

        for(int i = 0; i < amountOfEnemiesPerWave; i++)
        {
            enemySpawn.SpawnEnemy();
            yield return new WaitForSeconds(spawnTimer);
        }

        coRoutineHasStarted = false;
    }
}

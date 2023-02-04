using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    private int currentWave;
    private int waveValue;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateWave()
    {
        waveValue = currentWave * 10;
        GenerateEnemies();
    }

    public void GenerateEnemies()
    {
        // Generate a temporary list of enemies to generate
        //
        // in a loop grab a random enemy
        // see if we can afford it
        // if we can, add it to our list

        // repeat...

        // -> if we have no points left, leave the loop

        List<GameObject> generatedEnemies = new List<GameObject>();
        while(waveValue > 0)
        {
            if (waveValue >= 0)
            {
                generatedEnemies.Add(enemies[].enemyPrefab);
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
}

public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyType> enemies = new List<EnemyType>();
    [SerializeField] private int currentWave;
    [SerializeField] private int waveValue;
    [SerializeField] private List<GameObject> enemiesToSpawn = new List<GameObject>();

    [SerializeField] private Transform spawnLocation;
    [SerializeField] private int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();
    private void Start()
    {
        GenerateWave();
    }
    //FixedUpdate for more accurate timers
    private void FixedUpdate()
    {
        if(spawnTimer <= 0)
        {
            if(enemiesToSpawn.Count > 0)
            {
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLocation.position, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.deltaTime;
            waveTimer -= Time.deltaTime;
        }

        if (waveTimer <= 0 && AreAllEnemiesDead())
        {
            currentWave++;
            spawnedEnemies.Clear();
            GenerateWave();
        }
    }
    private bool AreAllEnemiesDead()
    {
        foreach(GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }

        return true;
    }
    private void GenerateWave()
    {
        waveValue = currentWave * 10;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }
    private void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while(waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if(waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyCost].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if(waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
}

[System.Serializable]
public class EnemyType
{
    public GameObject enemyPrefab;
    public int cost;
}
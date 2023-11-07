using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    public GameObject[] miniEnemiesPrefabs;
    public GameObject bossPrefab;
    private float spawnRange = 9;
    public int enemyCount;    
    public int waveNumber = 1;
    public int bossRound;    

    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }       
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float randomPositionX = Random.Range(-spawnRange, spawnRange);
        float randomPositionZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(randomPositionX, 0, randomPositionZ);
        return spawnPosition;
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        int randompowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randompowerup], GenerateSpawnPosition(), Quaternion.identity);
        
        for (int i = 0; i <enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), Quaternion.identity);
        }
    }

    private void SpawnBossWave(int currentRound)
    {
        int miniEnemiesToSpawn;
        if (bossRound != 0)
        {
            miniEnemiesToSpawn = currentRound / bossRound;           
        } else
        {
            miniEnemiesToSpawn = 1;
        }

        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(), Quaternion.identity);
        boss.GetComponent<Enemy>().miniEnemiesSpawnCount = miniEnemiesToSpawn;
    }

    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMiniEnemy = Random.Range(0, miniEnemiesPrefabs.Length);
            Instantiate(miniEnemiesPrefabs[randomMiniEnemy], GenerateSpawnPosition(), Quaternion.identity);
        }
    }


}

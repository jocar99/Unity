using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRange = 9;

    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    public GameObject bossPrefab;
    public int enemyCount;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyToSpawn = Random.Range(0, enemyPrefabs.Length);

            Instantiate(enemyPrefabs[enemyToSpawn], GenerateSpawnPosition(), enemyPrefabs[enemyToSpawn].transform.rotation);
        }
    }

    void SpawnPowerup()
    {
        int powerupToSpawn = Random.Range(0, powerupPrefabs.Length);

        Instantiate(powerupPrefabs[powerupToSpawn], GenerateSpawnPosition(), powerupPrefabs[powerupToSpawn].transform.rotation);
    }

    void SpawnBoss()
    {
        Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            if (waveNumber % 5 != 0)
            {
                SpawnEnemyWave(waveNumber);
                SpawnPowerup();
            }
            else
            {
                SpawnBoss();
                SpawnPowerup();
            }          
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}

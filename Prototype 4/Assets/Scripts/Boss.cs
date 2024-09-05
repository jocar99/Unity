using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnMinions", 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnMinions()
    {
        int enemyToSpawn = Random.Range(0, enemyPrefabs.Length);

        Vector3 spawnPosition = transform.position + new Vector3(0, 0, 2);

        Instantiate(enemyPrefabs[enemyToSpawn], spawnPosition, enemyPrefabs[enemyToSpawn].transform.rotation);
    }
}

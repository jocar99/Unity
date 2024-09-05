using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;

    private float spawnRangeX = 20;
    private float spawnPosZ = 20;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    private float spawnRangeZ = 15;
    private float spawnPozX = 30;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);

        Vector3 spawnPos;
        Quaternion spawnRotation;

        int spawnSide = Random.Range(0, 4);

        if (spawnSide == 0 || spawnSide == 1)
        {
            spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
            spawnRotation = animalPrefabs[animalIndex].transform.rotation;
        }
        else if (spawnSide == 2)
        {
            spawnPos = new Vector3(-spawnPozX, 0, Random.Range(0.0f, spawnRangeZ));
            spawnRotation = animalPrefabs[animalIndex].transform.rotation;
            spawnRotation.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            spawnPos = new Vector3(spawnPozX, 0, Random.Range(0.0f, spawnRangeZ));
            spawnRotation = animalPrefabs[animalIndex].transform.rotation;
            spawnRotation.eulerAngles = new Vector3(0, 270, 0);
        }

        Instantiate(animalPrefabs[animalIndex], spawnPos, spawnRotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject SpherePrefab;
    [SerializeField] private float startDelay = 2.0f;
    [SerializeField] private float repeatRate = 2.0f;
    [SerializeField] private float zRange = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBall", startDelay, repeatRate);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnBall()
    {
        Vector3 spawnPos = new Vector3(0, 15, Random.Range(-zRange, zRange));

        Instantiate(SpherePrefab, spawnPos, SpherePrefab.transform.rotation);
    }
}

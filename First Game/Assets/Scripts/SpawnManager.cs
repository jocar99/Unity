using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private float distance = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRepeating());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRepeating()
    {
        yield return new WaitForSeconds(Random.Range(0.45f, 0.55f));

        SpawnEnemy();

        StartCoroutine(SpawnRepeating());
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, GeneratePosition(), enemyPrefab.transform.rotation);
    }

    private Vector3 GeneratePosition()
    {
        Vector3 position = new Vector3();

        float angle = Random.Range(0, 2 * Mathf.PI);

        position.x = distance * Mathf.Cos(angle);
        position.x += player.transform.position.x;
        position.z = distance * Mathf.Sin(angle);
        position.z += player.transform.position.z;
        position.y = enemyPrefab.transform.position.y;
        

        return position;
    }
}

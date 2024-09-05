using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    private float lastDogSpawnTime;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && ((Time.time - lastDogSpawnTime) > 1))
        {
            lastDogSpawnTime = Time.time;
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        }
    }
}

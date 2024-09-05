using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float destroyDistance = 30.0f;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}

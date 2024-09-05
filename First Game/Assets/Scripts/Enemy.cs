using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float pushedSpeed = 20.0f;
    private int healthPoints = 30;

    public bool pushed;
    public Vector3 pushedPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!pushed)
        {
            MoveTowardsPlayer();
        }
        else
        {
            MoveToPushedPlace();
        }
        if(Vector3.Distance(transform.position, player.transform.position) > 200)
        {
            Destroy(gameObject);
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;

        direction.y = 0;
        direction = direction.normalized;

        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            healthPoints -= other.gameObject.GetComponent<Bullet>().damage;
            Destroy(other.gameObject);
            if(healthPoints <= 0)
            {
                Vector3 pickupSpawnPos = 
                    new Vector3(transform.position.x, pickupPrefab.transform.position.y, transform.position.z);
                Instantiate(pickupPrefab, pickupSpawnPos, pickupPrefab.transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void MoveToPushedPlace()
    {
        transform.position = Vector3.MoveTowards(transform.position, pushedPosition, pushedSpeed * Time.deltaTime);
        if(transform.position == pushedPosition)
        {
            pushed = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;

    public float speed = 3.0f;
    public float bulletStrength = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        enemyRb.AddForce(lookDirection * speed);

        if(transform.position.y < -5)
        {
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].GetComponent<Bullet>().targetEnemy == gameObject)
                {
                    Destroy(bullets[i]);
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            Vector3 awayFromBullet = (transform.position - player.transform.position).normalized;

            enemyRb.AddForce(awayFromBullet * bulletStrength, ForceMode.Impulse);
            Destroy(other.gameObject);
        }
    }
}

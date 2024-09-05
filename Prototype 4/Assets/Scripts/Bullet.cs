using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject targetEnemy;
    public float speed = 15.0f;

    private Rigidbody bulletRb;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan((targetEnemy.transform.position.x - transform.position.x)
            / (targetEnemy.transform.position.z - transform.position.z)) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(90, angle, 0);
        

        Vector3 moveDirection = (targetEnemy.transform.position - transform.position).normalized;

        bulletRb.AddForce(moveDirection * speed);
    }
}

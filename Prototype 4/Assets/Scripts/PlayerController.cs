using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerupStrength = 15.0f;

    public float speed = 5.0f;
    public float jumpStrength = 15.0f;
    public float slamStrength = 15.0f;
    public bool hasPowerup = false;
    public bool hasProjectilePowerup;
    public GameObject powerupIndicator;
    public GameObject projectilePowerupIndicator;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        projectilePowerupIndicator.transform.position = transform.position + new Vector3(0, 1, 0);

        if(Input.GetKeyDown(KeyCode.Space) && hasProjectilePowerup)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            for(int i = 0; i < enemies.Length; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);

                Bullet bulletScript = bullet.GetComponent<Bullet>();

                bulletScript.targetEnemy = enemies[i];
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SlamPowerup"))
        {
            Destroy(other.gameObject);
            playerRb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

            StartCoroutine(WaitAndSlam());
        }
        if(other.CompareTag("ProjectilePowerup"))
        {
            hasProjectilePowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(ProjectileCountdownRoutine());
            projectilePowerupIndicator.gameObject.SetActive(true);
        }

        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator WaitAndSlam()
    {
        yield return new WaitForSeconds(0.1f);
        playerRb.AddForce(Vector3.down * jumpStrength * 2, ForceMode.Impulse);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Number of enemies:" + enemies.Length);

        yield return new WaitForSeconds(0.1f);

        for(int i = 0; i < enemies.Length; i++)
        {
            float distance = Mathf.Pow((enemies[i].transform.position.x - transform.position.x), 2) +
                Mathf.Pow((enemies[i].transform.position.z - transform.position.z), 2);

            Debug.Log(distance);

            if(distance < 100)
            {
                Vector3 awayFromPlayer = (enemies[i].transform.position - transform.position).normalized;

                Rigidbody enemyRb = enemies[i].GetComponent<Rigidbody>();

                enemyRb.AddForce(awayFromPlayer * slamStrength / distance, ForceMode.Impulse);
                Debug.Log("Slamed");
            }
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    IEnumerator ProjectileCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasProjectilePowerup = false;
        projectilePowerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}

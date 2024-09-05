using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int playerHealth = 3;
    private int playerXP = 0;
    private GameManager gameManager;
    [SerializeField] private float pushDistance = 5.0f;
    [SerializeField] private float distanceCap = 20.0f;
    [SerializeField] private Camera mainCamera;

    public int gunDamage = 10;
    public float fireRate = 200.0f;
    public float movementSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movementDirection = new Vector3(verticalInput, 0, -horizontalInput).normalized;

        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World); 

        RaycastHit hit;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 rotation = Quaternion.LookRotation(hit.point - transform.position).eulerAngles;
            rotation.x = 0f;
            rotation.z = 0f;

            transform.rotation = Quaternion.Euler(rotation);
        }

        if(playerXP >= gameManager.nextLevel)
        {
            gameManager.LevelUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pickup"))
        {
            playerXP++;
            Destroy(other.gameObject);
            Debug.Log(playerXP);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth--;
            Debug.Log(playerHealth);
            PushEnemies();
            if (playerHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void PushEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        for(int i = 0; i < enemies.Length; i++)
        {
            Vector3 playerPositionWithoutY = transform.position;
            playerPositionWithoutY.y = 0;

            Vector3 enemyPositionWithoutY = enemies[i].transform.position;
            enemyPositionWithoutY.y = 0;

            float distance = Vector3.Distance(enemyPositionWithoutY, playerPositionWithoutY);
            if(distance < distanceCap)
            {
                Vector3 direction = (enemyPositionWithoutY - playerPositionWithoutY).normalized;
                direction *= pushDistance;

                enemies[i].GetComponent<Enemy>().pushed = true;
                enemies[i].GetComponent<Enemy>().pushedPosition = enemies[i].transform.position + direction;
            }

        }

    }

}

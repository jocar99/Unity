using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(CalculateDistanceToPlayer() < 5)
        {
            MoveToPlayer();
        }
    }

    private float CalculateDistanceToPlayer()
    {
        Vector3 playerPosWithoutY = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 pickupPosWithoutY = new Vector3(transform.position.x, 0, transform.position.z);

        return Vector3.Distance(playerPosWithoutY, pickupPosWithoutY);
    }

    private void MoveToPlayer()
    {
        Vector3 playerPosWithPickupY = 
            new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);


        transform.position = 
            Vector3.MoveTowards(transform.position, playerPosWithPickupY, speed * Time.deltaTime);
    }
}

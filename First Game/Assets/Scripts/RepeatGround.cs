using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatGround : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x - transform.position.x > 400)
        {
            transform.position = new Vector3(transform.position.x + 600, transform.position.y, transform.position.z);
        }
        else if (player.transform.position.x - transform.position.x < -400)
        {
            transform.position = new Vector3(transform.position.x - 600, transform.position.y, transform.position.z);
        }
        else if (player.transform.position.z - transform.position.z > 400)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 600);
        }
        else if (player.transform.position.z - transform.position.z < -400)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 600);
        }
    }
}

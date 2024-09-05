using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootingPoint;
    [SerializeField] private bool gunReady = true;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMovement playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && gunReady)
        {
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        gunReady = false;
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.transform.position, CalculateAngle());
        bullet.GetComponent<Bullet>().damage = playerScript.gunDamage;
        StartCoroutine(WaitForGunReady());
    }

    IEnumerator WaitForGunReady()
    {
        yield return new WaitForSeconds(100.0f / playerScript.fireRate);
        gunReady = true;
    }

    private Quaternion CalculateAngle()
    {
        GameObject parent = transform.parent.gameObject;

        Vector3 eulerRotation = parent.transform.rotation.eulerAngles;

        eulerRotation.x = 90;

        Quaternion rotation = Quaternion.Euler(eulerRotation);

        return rotation;
    }
}

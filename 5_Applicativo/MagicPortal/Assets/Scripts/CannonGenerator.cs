using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CannonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cannonPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int cannonCount;
    [SerializeField] private float fireInterval;
    [SerializeField] private float fireSpeed;
    [SerializeField] private float fireHeight;
    [SerializeField] private Vector3 startingPosition = new Vector3(0, 0, 0);
    [SerializeField] private float bulletLifetime;

    private GameObject[] cannons;
    private List<GameObject> bullets = new List<GameObject>();

    void Start()
    {
        cannons = new GameObject[cannonCount];
        for (int i = 0; i < cannonCount; i++)
        {
            Vector3 cannonPosition = startingPosition + new Vector3(i * 2.0f, fireHeight, 0); //distanza fra i cannoni
            cannons[i] = Instantiate(cannonPrefab, cannonPosition, Quaternion.identity);
        }
        StartCoroutine(ShootCannons());
    }

    void Update()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i] != null)
            {
                bullets[i].transform.Translate(Vector3.back * fireSpeed * Time.deltaTime);
                if (bullets[i].transform.position.z < -20f)
                {
                    Destroy(bullets[i]);
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    IEnumerator ShootCannons()
    {
        while (true)
        {
            for (int i = 0; i < cannonCount; i += 2)
            {
                FireBullet(cannons[i].transform.position);
            }
            yield return new WaitForSeconds(fireInterval);

            for (int i = 1; i < cannonCount; i += 2)
            {
                FireBullet(cannons[i].transform.position);
            }
            yield return new WaitForSeconds(fireInterval);
        }
    }

    void FireBullet(Vector3 position)
    {
        Vector3 bulletSpawnPosition = position + new Vector3(0, 0, 0f);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);
        bullets.Add(bullet);
        Destroy(bullet, bulletLifetime);
    }
}

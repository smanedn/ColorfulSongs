using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static Unity.Collections.AllocatorManager;
using System.Linq;

public class CannonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject generator;
    [SerializeField] private GameObject cannonPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject floor;
    [SerializeField] private int cannonCount;
    [SerializeField] private float fireInterval;
    [SerializeField] private float fireSpeed;
    [SerializeField] private float fireHeight;
    [SerializeField] private float bulletLifetime;
    private int startingX;
    private int endingX;
    private int startingZ;
    private int endingZ;
    private int startingY;

    private GameObject[] cannons;
    private List<GameObject> bullets = new List<GameObject>();

    void Start()
    {
        generator.GetComponent<TerrainGenerator>().setVariable(startingX, startingZ, endingX, endingZ, startingY, "CannonGenerator");

        int[] posX = new int[cannonCount];
        cannons = new GameObject[cannonCount];
        for (int i = 0; i < cannonCount; i++)
        {
            Vector3 cannonPosition = new Vector3(startingX, startingY, endingZ) + new Vector3(i * 2.0f, fireHeight, 0); //distanza fra i cannoni (2.0f)
            cannons[i] = Instantiate(cannonPrefab, cannonPosition, Quaternion.identity);
            cannons[i].name = "Cannone" + i;
            cannons[i].transform.SetParent(parent.transform);
        }
        StartCoroutine(ShootCannons());

        for (int x = startingX; x < endingX; x++)
        {
            for (int z = startingZ; z <= endingZ; z++)
            {
                var cube = Instantiate(floor, new Vector3(x, startingY, z), Quaternion.identity);
                cube.name = "floor[" + x + "; " + z + "]";
                cube.transform.SetParent(parent.transform);                     
            }
        }
        print("Cannon Generator:[X start: " + startingX + " X fine: " + endingX + "] && [Z start: " + startingZ + " Z fine: " + endingZ + "]");

    }

    void Update()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i] != null)
            {
                bullets[i].transform.Translate(Vector3.back * fireSpeed * Time.deltaTime);
                bullets[i].transform.SetParent(parent.transform);
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

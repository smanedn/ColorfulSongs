using UnityEngine;
using System.Collections.Generic;

public class TrumpetEnemy : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Spawn settings")]
    [SerializeField] private Vector3 enemySpawnPosition = Vector3.zero;
    [SerializeField] private Transform parent;

    [Header("FirePoint Offset")]
    [SerializeField] private Vector3 firePointOffset = new Vector3(0, 0, 1);

    [Header("Shooting Settings")]
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float timeBetweenShots = 0.1f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletLifetime = 3f;

    private GameObject enemyInstance;
    private float shootTimer = 0f;
    private List<GameObject> bullets = new List<GameObject>();

    void Start()
    {
        enemyInstance = Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity);
        if (parent != null) enemyInstance.transform.SetParent(parent);
    }

    void Update()
    {
        if (enemyInstance == null) return;

        enemyInstance.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            ShootBullet();
            shootTimer = timeBetweenShots;
        }

        foreach (var bullet in bullets)
        {
            if (bullet != null)
            {
                bullet.transform.Translate(bullet.transform.forward * bulletSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    void ShootBullet()
    {
        Vector3 firePointWorldPos = enemyInstance.transform.position + enemyInstance.transform.TransformDirection(firePointOffset);

        GameObject bullet = Instantiate(bulletPrefab, firePointWorldPos, enemyInstance.transform.rotation);
        if (parent != null) bullet.transform.SetParent(parent);

        bullets.Add(bullet);
        Destroy(bullet, bulletLifetime);
    }
}

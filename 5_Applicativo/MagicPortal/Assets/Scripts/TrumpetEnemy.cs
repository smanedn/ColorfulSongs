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

    [Header("Turret Shooting Settings")]
    [SerializeField] private float rotationSpeed = 90f; // gradi al secondo
    [SerializeField] private float timeBetweenShots = 0.1f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletLifetime = 3f;

    private GameObject enemyInstance;
    private Transform firePoint;
    private float shootTimer = 0f;
    private List<GameObject> bullets = new List<GameObject>();

    void Start()
    {
        // Spawn del nemico
        enemyInstance = Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity);
        if (parent != null) enemyInstance.transform.SetParent(parent);

        // Crea un firePoint al centro del nemico
        GameObject firePointObj = new GameObject("FirePoint");
        firePointObj.transform.SetParent(enemyInstance.transform);
        firePointObj.transform.localPosition = Vector3.zero;
        firePoint = firePointObj.transform;
    }

    void Update()
    {
        if (enemyInstance == null) return;

        // Ruota il nemico continuamente
        enemyInstance.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            ShootBullet();
            shootTimer = timeBetweenShots;
        }

        // Muove i proiettili in avanti
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
        Vector3 direction = enemyInstance.transform.forward;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction));
        if (parent != null) bullet.transform.SetParent(parent);

        bullets.Add(bullet);
        Destroy(bullet, bulletLifetime);
    }
}

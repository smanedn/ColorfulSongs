using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    private float enemyX;
    private float y;
    private float enemyZ;
    private int startX;
    private int endX;
    private int startZ;
    private int endZ;

    private float timer = 0f;
    private int enemyPosition = 0; //0=base 1=in salto 2=arrivato REPEAT

    private Vector3 position;
    private float[][] positions;
    [SerializeField] private GameObject generator;
    private void Start()
    {

        startX = generator.GetComponent<TerrainGenerator>().getEndX1();
        endX = generator.GetComponent<TerrainGenerator>().getStartX2();
        startZ = 0;
        endZ = generator.GetComponent<TerrainGenerator>().getEndZ();
        enemyX = startX + (endX - startX) / 2;
        enemyZ = endZ / 2 - 0.5f;
        y = 0.5f;

        positions[0][0] = startX;
        positions[0][1] = enemyX;
        positions[0][2] = endX;

        positions[1][0] = startZ;
        positions[1][1] = enemyZ;
        positions[1][2] = endZ;

        position = new Vector3(enemyX, y, enemyZ);
    }
    void Update()
    {
        timer = Time.fixedTime;
        if (Mathf.RoundToInt(timer) % 6 == 0 && timer!=0)
        {
            print(Mathf.RoundToInt(timer) + "%6 = 0");
            float diff = enemyZ - startZ;
            position = new Vector3(enemyX, y + 2f, startZ + diff / 2);
        }
        else if (Mathf.RoundToInt(timer) % 9 == 0)
        {
            print(Mathf.RoundToInt(timer) + "%9 = 0");
            position = new Vector3(enemyX, y, enemyZ);
        }
        else if (Mathf.RoundToInt(timer) % 3 == 0)
        {
            print(Mathf.RoundToInt(timer) + "%3 = 0");
            position = new Vector3(enemyX, y, startZ);
        }
       

        GetComponent<Rigidbody>().transform.position = position;

    }
}

using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float enemyX;
    private float y;
    private float enemyZ;
    private float startX;
    private float endX;
    private float startZ;
    private float endZ;

    private int p1;
    private int p2 = 0;

    private float p1X;
    private float p1Z;
    private float p2X;
    private float p2Z;

    private int enemyPosition = 0;

    private Vector3 position;
    //private float[][] positions;
    [SerializeField] private GameObject generator;
    private void Start()
    {

        startX = generator.GetComponent<TerrainGenerator>().getEndX1()+0.5f;
        endX = generator.GetComponent<TerrainGenerator>().getStartX2()-0.5f;
        startZ = 0.5f;
        endZ = generator.GetComponent<TerrainGenerator>().getEndZ()-2;
        enemyX = startX + (endX - startX) / 2;
        enemyZ = endZ / 2 - 0.5f;
        y = 0.5f;

        position = new Vector3(enemyX, y, enemyZ);
        StartCoroutine(MovementDrum());
    }

    IEnumerator MovementDrum()
    {
        float[,] positions = {
            {enemyX,enemyZ},
            {endX,enemyZ},
            {endX,startZ},
            {enemyX,startZ},
            {startX,startZ},
            {startX,enemyZ},
            {startX,endZ},
            {enemyX,endZ},
            {endX,endZ}
            };

        while (true)

        {
            if(enemyPosition == 0)
            {

                do
                {
                    p1 = p2;
                    p2 = Random.Range(0, 9);
                } while (p1 == p2);

                p1X = positions[p1,0];
                p1Z = positions[p1,1];

                p2X = positions[p2,0];
                p2Z = positions[p2,1];
                enemyPosition++;
            }
            if (enemyPosition == 1)
            {
                position = new Vector3(p1X, y, p1Z);
                enemyPosition++;
                
            }
            else if (enemyPosition == 2 || enemyPosition == 4)
            {
                float diffZ = p2Z - p1Z;
                float diffX = p2X - p1X;
                position = new Vector3(p1X + diffX/2, y + 2f, p1Z + diffZ / 2);
                if (enemyPosition == 1)
                {
                    enemyPosition++;
                }
                else
                {
                    enemyPosition = 0;
                }
            }
            else if (enemyPosition == 3)
            {
                position = new Vector3(enemyX, y, startZ);
                enemyPosition++;
            }
            GetComponent<Rigidbody>().transform.position = position;
            yield return new WaitForSeconds(0.5f);
        }
    }
}

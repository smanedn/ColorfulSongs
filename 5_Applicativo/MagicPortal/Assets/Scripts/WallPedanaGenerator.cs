using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WallPedanaGenerator : MonoBehaviour
{
    [SerializeField] private int startingX;   //= 5
    [SerializeField] private int endingX;     //= 20
    [SerializeField] private int startingZ;   //= 0
    [SerializeField] private int endingZ;     //= 12
    [SerializeField] private int startingY;   //= 0;

    [SerializeField] private GameObject ladder;
    [SerializeField] private GameObject pedana;
    [SerializeField] private GameObject griglia;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject parent;

    void Start()
    {
        int[] posX = new int[endingX - startingX];
        int[] posZ = new int[endingX - startingX];

        int oldZ = 0;
        bool first = true;
        int i = 0;
        int zBlocco = 0;

        var scala = Instantiate(ladder, new Vector3(startingX-1, startingY+2.5f, endingZ-0.5f), Quaternion.identity);
        scala.name = "ladder[" + startingX + "; " + endingZ + "]";

        scala.transform.SetParent(parent.transform);

        for (int x = startingX; x < endingX; x++)
        {
            if (first)
            {
                zBlocco = Random.Range(startingZ + 1, endingZ);
                first = false;
            }
            else
            {
                do
                {
                    zBlocco = Random.Range(oldZ - 1, oldZ + 2);
                } while (zBlocco >= endingZ || zBlocco <= startingZ);
            }
            oldZ = zBlocco;

            for (int z = startingZ; z <= endingZ; z++)
            {
                if (z == zBlocco)
                {
                    var percorso = Instantiate(pedana, new Vector3(x, startingY, z), Quaternion.identity);
                    percorso.name = "pedana[" + x + "; " + z + "]";
                    posX[i] = x;
                    posZ[i] = z;

                    percorso.transform.SetParent(parent.transform);
                    i++;
                }
                var cube = Instantiate(griglia, new Vector3(x, startingY, z), Quaternion.identity);
                cube.name = "griglia[" + x + "; " + z + "]";

                cube.transform.SetParent(parent.transform);
            }
        }
        for (int j = 0; j < posX.Length; j++)
        {
            var cube = Instantiate(wall, new Vector3(posX[j], posZ[j]+1, endingZ - 0.1f), Quaternion.identity);
            var xName = startingX + j;
            cube.name = "pedanaWall[" + xName + "; " + posX[j] + " ; " + endingZ + "]";
            cube.transform.SetParent(parent.transform);
        }
    }
}

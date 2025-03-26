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

    [SerializeField] private GameObject pedana;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject parent;

    void Start()
    {
        int[] posX = new int[endingX - startingX];
        int[] posZ = new int[endingX - startingX];

        int x = startingX;
        int oldZ = 0;
        bool first = true;
        int i = 0;
        int z = 0;
        while (x < endingX)
        {
            if (first)
            {
                z = Random.Range(startingZ+1, endingZ);
                first = false;
            }
            else
            {
                Debug.Log(z + ";" + endingZ);
                Debug.Log(z + ";" + endingZ);
                do
                {
                    z = Random.Range(oldZ - 1, oldZ + 2);
                } while (z >= endingZ || z <= startingZ);
                

            }
            oldZ = z;

            var cube = Instantiate(pedana, new Vector3(x, startingY, z), Quaternion.identity);
            cube.name = "pedana[" + x + "; " + z + "]";
            posX[i] = x;
            posZ[i] = z;

            cube.transform.SetParent(parent.transform);
            i++;
            x++;
        }

        for (int j = 0; j < posX.Length; j++)
        {
            var cube = Instantiate(wall, new Vector3(posX[j], posZ[j], endingZ-0.1f), Quaternion.identity);
            var xName = startingX + j;
            cube.name = "pedanaWall[" + xName + "; " + posX[j] + " ; "+ endingZ + "]";
            cube.transform.SetParent(parent.transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FakeFloorGenerator : MonoBehaviour
{
    [SerializeField] private int startingX;   //= 5
    [SerializeField] private int endingX;     //= 20
    [SerializeField] private int startingZ;   //= 0
    [SerializeField] private int endingZ;     //= 12
    [SerializeField] private int startingY;   //= 0;

    [SerializeField] private GameObject realFloor;
    [SerializeField] private GameObject fakeFloor;
    [SerializeField] private GameObject parent;

    void Start()
    {
        int[] posZ = new int[2];
        int oldZ = 0;
        bool first = true;
        int zBlock = 0;

        for (int x = startingX; x < endingX; x++)
        {
            if (first)
            {
                for(int i = 0; i < 2; i++)
                {
                    zBlock = Random.Range(startingZ + 1, endingZ);
                    posZ[i] = zBlock;
                }
                
                first = false;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    oldZ = posZ[i];
                    do
                    {
                        zBlock = Random.Range(oldZ - 1, oldZ + 2);
                    } while (zBlock >= endingZ || zBlock <= startingZ);
                      
                    posZ[i] = zBlock;
                }
            }
            oldZ = zBlock;

            for (int z = startingZ; z <= endingZ; z++)
            {
                if (posZ.Contains(z))
                {
                    var percorso = Instantiate(realFloor, new Vector3(x, startingY, z), Quaternion.identity);
                    percorso.name = "realFloor[" + x + "; " + z + "]";

                    percorso.transform.SetParent(parent.transform);
                }
                else
                {
                    var cube = Instantiate(fakeFloor, new Vector3(x, startingY, z), Quaternion.identity);
                    cube.name = "fakeFloor[" + x + "; " + z + "]";

                    cube.transform.SetParent(parent.transform);
                }
                
            }
        }
    }
}
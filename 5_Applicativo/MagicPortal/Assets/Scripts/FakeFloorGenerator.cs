/*using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FakeFloorGenerator : MonoBehaviour
{
    private int startingX;   //= 5
    private int endingX;     //= 20
    private int startingZ;   //= 0
    private int endingZ;     //= 12
    private int startingY;   //= 0;

    [SerializeField] private GameObject realFloor;
    [SerializeField] private GameObject fakeFloor;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject generator;

    void Start()
    {
        startingX = generator.GetComponent<TerrainGenerator>().getStartX("FakeFloorGenerator");
        startingZ = generator.GetComponent<TerrainGenerator>().getStartZ("FakeFloorGenerator");
        startingY = generator.GetComponent<TerrainGenerator>().getStartY("FakeFloorGenerator");
        endingX = generator.GetComponent<TerrainGenerator>().getEndX("FakeFloorGenerator");
        endingZ = generator.GetComponent<TerrainGenerator>().getEndZ("FakeFloorGenerator");
        print("FAKEFLOORGENERATOR\nStartX: "+ startingX);

        int[] posZ = new int[2];
        int oldZ = 0;
        bool first = true;
        int blockZ = 0;

        for (int x = startingX; x < endingX; x++)
        {
            if (first)
            {
                for(int i = 0; i < 2; i++)
                {
                    blockZ = Random.Range(startingZ + 1, endingZ);
                    posZ[i] = blockZ;
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
                        blockZ = Random.Range(oldZ - 1, oldZ + 2);
                    } while (blockZ >= endingZ || blockZ <= startingZ);
                      
                    posZ[i] = blockZ;
                }
            }
            oldZ = blockZ;

            for (int z = startingZ; z <= endingZ; z++)
            {
                if (posZ.Contains(z))
                {
                    var path = Instantiate(realFloor, new Vector3(x, startingY, z), Quaternion.identity);
                    path.name = "realFloor[" + x + "; " + z + "]";

                    path.transform.SetParent(parent.transform);
                }
                else
                {
                    var cube = Instantiate(fakeFloor, new Vector3(x, startingY, z), Quaternion.identity);
                    cube.name = "fakeFloor[" + x + "; " + z + "]";

                    cube.transform.SetParent(parent.transform);
                }
                
            }
        }
        print("FakeFloorGenerator:[X start: " + startingX + " X fine: " + endingX + "] && [Z start: " + startingZ + " Z fine: " + endingZ + "]");
    }
}**/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FakeFloorGenerator : MonoBehaviour
{
    private int startingX;   
    private int endingX;     
    private int startingZ;   
    private int endingZ;     
    private int startingY;   

    [SerializeField] private GameObject realFloor;
    [SerializeField] private GameObject fakeFloor;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject generator;

    void Start()
    {
        startingX = generator.GetComponent<TerrainGenerator>().getStartX("FakeFloorGenerator");
        startingZ = generator.GetComponent<TerrainGenerator>().getStartZ("FakeFloorGenerator");
        startingY = generator.GetComponent<TerrainGenerator>().getStartY("FakeFloorGenerator");
        endingX = generator.GetComponent<TerrainGenerator>().getEndX("FakeFloorGenerator");
        endingZ = generator.GetComponent<TerrainGenerator>().getEndZ("FakeFloorGenerator");


        int[] posZ = new int[2];
        int oldZ = 0;
        bool first = true;
        int zBlock = 0;

        for (int x = startingX; x < endingX; x++)
        {
            if (first)
            {
                for (int i = 0; i < 2; i++)
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
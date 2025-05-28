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
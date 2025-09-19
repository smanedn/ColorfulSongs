using UnityEngine;

public class WallPedanaGenerator : MonoBehaviour
{
    private int startingX;   //= 5
    private int endingX;     //= 20
    private int startingZ;   //= 0
    private int endingZ;     //= 12
    private int startingY;   //= 0;

    [SerializeField] private GameObject realBlock;
    [SerializeField] private GameObject fakeBlock;
    [SerializeField] private GameObject firstBlock;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject generator;

    void Start()
    {
        startingX = generator.GetComponent<TerrainGenerator>().getStartX("WallPedanaGenerator");
        startingZ = generator.GetComponent<TerrainGenerator>().getStartZ("WallPedanaGenerator");
        startingY = generator.GetComponent<TerrainGenerator>().getStartY("WallPedanaGenerator");
        endingX = generator.GetComponent<TerrainGenerator>().getEndX("WallPedanaGenerator");
        endingZ = generator.GetComponent<TerrainGenerator>().getEndZ("WallPedanaGenerator")-3;
        //-2 perch� non vogliamo riempire le due file vicino al muro

        int[] posX = new int[endingX - startingX];
        int[] posZ = new int[endingX - startingX];

        int oldZ = 0;
        bool first = true;
        bool generateFirst = true;
        int i = 0;
        int blockZ = 0;

        for (int x = startingX; x < endingX; x++)
        {
            if (first)
            {
                blockZ = Random.Range(startingZ + 1, endingZ);
                first = false;
            }
            else
            {
                do
                {
                    blockZ = Random.Range(oldZ - 1, oldZ + 2);
                } while (blockZ >= endingZ || blockZ <= startingZ);
            }
            oldZ = blockZ;

            for (int z = startingZ; z <= endingZ; z++)
            {
                if (z == blockZ)
                {
                    var block = gameObject;
                    if (generateFirst)
                    {
                        block = firstBlock;
                        generateFirst = false;
                    }
                    else
                    {
                        block = realBlock;
                    }
                    var percorso = Instantiate(block, new Vector3(x, startingY, z), Quaternion.identity);
                    percorso.name = "realBlock[" + x + "; " + z + "]";
                    posX[i] = x;
                    posZ[i] = z;

                    percorso.transform.SetParent(parent.transform);
                    i++;
                }
                var cube = Instantiate(fakeBlock, new Vector3(x, startingY, z), Quaternion.identity);
                cube.name = "fakeBlock[" + x + "; " + z + "]";

                cube.transform.SetParent(parent.transform);
            }
        }
        for (int j = 0; j < posX.Length; j++)
        {
            var cube = Instantiate(wall, new Vector3(posX[j], posZ[j]+1, endingZ + 2.9f), Quaternion.identity);
            var wallX = startingX + j;
            cube.name = "realBlockWall[" + wallX + "; " + posX[j] + " ; " + endingZ + "]";
            cube.transform.SetParent(parent.transform);
        }
    }
}

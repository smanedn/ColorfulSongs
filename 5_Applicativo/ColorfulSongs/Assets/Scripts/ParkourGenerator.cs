using UnityEngine;

public class ParkourGenerator : MonoBehaviour
{
    private int startingX; 
    private int endingX;   
    private int startingZ; 
    private int endingZ;   
    private int startingY; 

    private CharacterController characterController;

    [SerializeField] private GameObject parkour;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject generator;

    void Start()
    {
        startingX = generator.GetComponent<TerrainGenerator>().getStartX("ParkourGenerator");
        startingZ = generator.GetComponent<TerrainGenerator>().getStartZ("ParkourGenerator");
        startingY = generator.GetComponent<TerrainGenerator>().getStartY("ParkourGenerator");
        endingX = generator.GetComponent<TerrainGenerator>().getEndX("ParkourGenerator");
        endingZ = generator.GetComponent<TerrainGenerator>().getEndZ("ParkourGenerator");

        int oldZ = 0;
        int z = 0;
        int x = 0;
        bool first = true;

        while(x < endingX)
        {
            if (first){
                x = startingX + Random.Range(0,3);
                z = Random.Range(startingZ, endingZ);
                first = false;
            }
            else
            {
                while(z>=endingZ || z < 0 || z==oldZ)
                {
                    z = Random.Range(oldZ-2, oldZ + 3);
                }
            }
            oldZ = z;
            
            var cube = Instantiate(parkour, new Vector3(x, startingY, z), Quaternion.identity);
            cube.name = "parkour[" + x + "; "+z+"]";
            cube.transform.SetParent(parent.transform);
            x += Random.Range(2,4);
            z = -1;
        }
    } 
}

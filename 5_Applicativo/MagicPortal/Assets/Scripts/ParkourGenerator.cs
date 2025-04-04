using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ParkourGenerator : MonoBehaviour
{
    private int startingX;   //= 5
    private int endingX;     //= 20
    private int startingZ;   //= 0
    private int endingZ;     //= 8
    private int startingY;   //= 0;

    private CharacterController characterController;

    [SerializeField] private GameObject parkour;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject generator;

    void Start()
    {
        //generator.GetComponent<TerrainGenerator>().setVariable(startingX, startingZ, endingX, this.endingZ, this.startingY, "ParkourGenerator");
        startingX = generator.GetComponent<TerrainGenerator>().getStartX1();
        int oldZ = -1;
        int z = 0;
        int x = 0;
        bool first = true;

        while(x < endingX)
        {
            if (first){
                x = startingX + Random.Range(0,3);
                first = false;
            }
            
            if (oldZ == -1)
            {
                z = Random.Range(startingZ, endingZ);
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

        print("Parkour Generator:[X start: " + startingX + " X fine: " + endingX + "] && [Z start: " + startingZ + " Z fine: " + endingZ + "]");
    } 
}

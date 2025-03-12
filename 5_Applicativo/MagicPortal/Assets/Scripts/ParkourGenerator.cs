using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ParkourGenerator : MonoBehaviour
{
    [SerializeField] private int startingX;   //= 5
    [SerializeField] private int endingX;     //= 20
    [SerializeField] private int startingZ;   //= 0
    [SerializeField] private int endingZ;     //= 12
    [SerializeField] private int startingY;   //= 0;
    

    private CharacterController characterController;

    [SerializeField] private GameObject parkour;
    [SerializeField] private GameObject parent;

    void Start()
    {
        int oldZ = -1;
        int z = 0;
        int x = 0;
        bool first = true;

        while(x<endingX)
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
    }

    
}

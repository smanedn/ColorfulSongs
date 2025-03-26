using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private int x;
    private int z;
    private int h;
    private bool empty;
    private int firstEnigma;
    private int secondEnigma;
  

    [SerializeField] private int row;         //= 30  //f=20
    [SerializeField] private int column;      //= 12
    [SerializeField] private int y;                   //f=-2

    [SerializeField] private int startingX;           //f=5
    [SerializeField] private int startingZ;
    [SerializeField] private int wallHeight;

    [SerializeField] private int xEnigma;     //= 5
    [SerializeField] private int lEnigma;     //= 15

    [SerializeField] private int xEnigma2;     //= 25
    [SerializeField] private int lEnigma2;     //= 10

    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject parent;

    public GameObject[] enigmasFirst;
    public GameObject[] enigmasSecond;

    void Start()
    {
        do
        {
            firstEnigma = Random.Range(0, 4);
            secondEnigma = Random.Range(0, 4);
        } while (firstEnigma == secondEnigma);

        enigmasFirst[firstEnigma].SetActive(true);
        enigmasSecond[secondEnigma].SetActive(true);

        for (int r = startingX; r <= row; r++)
        {
            x = r;
            if (
                ((r < xEnigma || r >= xEnigma + lEnigma) || xEnigma==0) &&
                ((r < xEnigma2 || r >= xEnigma2 + lEnigma2) || xEnigma2 == 0)
                )
            {
                empty = false;
            }
            else
            {
                empty = true;
            }

            for (int c = startingZ; c <= column; c++)
            {
                z = c;
                if (!empty)
                {
                    string name = "pavimento[" + r.ToString() + ";" + y.ToString() + ";" + c.ToString() + "]";
                    var cube = Instantiate(floor, new Vector3(x, y, z), Quaternion.identity);
                    cube.name = name;
                    cube.transform.SetParent(parent.transform); 
                }

                if (r == row || c == column)
                {
                    for (int a = y; a <= wallHeight; a++)
                    {
                        string nameWall = "wall[" + r.ToString() + ";" + a.ToString() + ";" + c.ToString() + "]";
                        var wallObj = Instantiate(wall, new Vector3(x, a, z), Quaternion.identity);
                        wallObj.name = nameWall;
                        wallObj.transform.SetParent(parent.transform);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

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

    [SerializeField] private int startEnigma1X;     //= 5
    [SerializeField] private int endEnigma1X;       //= 15
    [SerializeField] private int startEnigma1Z;     //= 0
    [SerializeField] private int endEnigma1Z;       //= 8
    [SerializeField] private int startEnigma1Y;     //= 0

    [SerializeField] private int startEnigma2X;     //= 25
    [SerializeField] private int endEnigma2X;       //= 35
    [SerializeField] private int startEnigma2Z;     //= 0
    [SerializeField] private int endEnigma2Z;       //= 8
    [SerializeField] private int startEnigma2Y;     //= 0

    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject parent;

    public GameObject[] enigmas;

    void Start()
    {
        do
        {
            firstEnigma = Random.Range(0, 4);
            secondEnigma = Random.Range(0, 4);
        } while (firstEnigma == secondEnigma);

        enigmas[firstEnigma].SetActive(true);
        enigmas[secondEnigma].SetActive(true);

        for (int r = startingX; r <= row; r++)
        {
            x = r;
            if (
                ((r < startEnigma1X || r >= endEnigma1X)) &&
                ((r < startEnigma2X || r >= endEnigma2X))
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

    public void setVariable(int startX, int endX, int startZ, int endZ, int startY, string name)
    {
        
        
        if (name == enigmas[firstEnigma].name)
        {
            print("Entrato nel primo IF "+enigmas[firstEnigma].name);
            startX = startEnigma1X;
            endX = endEnigma1X;
            startZ = startEnigma1Z;
            endZ = endEnigma1Z;
            startY = startEnigma1Y;
        }
        else if(name == enigmas[secondEnigma].name)
        {
            print("Entrato nel secondo IF " + enigmas[secondEnigma].name);
            startX = startEnigma2X;
            endX = endEnigma2X;
            startZ = startEnigma2Z;
            endZ = endEnigma2Z;
            startY = startEnigma2Y;
        }
        print("Function: " +name + ":[X start: " + startX + " X fine: " + endX + "] && [Z start: " + startZ + " Z fine: " + endZ + "]");

    }

    public int getStartX1() { return startEnigma1X; }
    public int getEndX1() { return endEnigma1X; }
    public int getStartY1() { return startEnigma1Y; }
    public int getStartZ1() { return startEnigma1Z; }
    public int getEndZ1() { return endEnigma1Z; }
    public int getStartY2() { return startEnigma2Y; }

    public int getStartX2() { return startEnigma2X; }
    public int getEndX2() { return endEnigma2X; }
    public int getStartZ2() { return startEnigma2Z; }
    public int getEndZ2() { return endEnigma2Z; }

}


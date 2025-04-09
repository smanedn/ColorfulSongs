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
    private int startingX = 0;
    private int startingZ = 0;


    [SerializeField] private int row;         //= 40  
    [SerializeField] private int column;      //= 8
    //[SerializeField] private int rowLShaped;         //= 30  
    //[SerializeField] private int columnLShaped;      //= 12
    [SerializeField] private int y;
    private int firstTerrainZ; // Uguale a LRow/2
    private int firstTerrainX; // Uguale a LColumn/2


    [SerializeField] private int wallHeight;

    [SerializeField] private int startEnigma1X;     //= 5
    [SerializeField] private int endEnigma1X;       //= 15
    private int startEnigma1Z;     //= 0
    private int endEnigma1Z;       //= COLUMN
    [SerializeField] private int startEnigma1Y;     //= 0

    [SerializeField] private int startEnigma2X;     //= 25
    [SerializeField] private int endEnigma2X;       //= 35
    private int startEnigma2Z;     //= 0
    private int endEnigma2Z;       //= COLUMN
    [SerializeField] private int startEnigma2Y;     //= 0

    private int startLShapedEnigma2X;     //= UGUALE A: firstTerrainX
    private int endLShapedEnigma2X;       //= UGUALE A: LRow
    private int startLShapedEnigma2Z;     //= UGUALE A: firstTerrainZ
    private int endLShapedEnigma2Z;       //= UGUALE A: LColumn

    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject finishPortal;

    public GameObject[] enigmas;

    void Start()
    {
        int l = enigmas.Length;
        do
        {
            firstEnigma = Random.Range(0, l);
            secondEnigma = Random.Range(0, l);
        } while (firstEnigma == secondEnigma);

        enigmas[firstEnigma].SetActive(true);
        enigmas[secondEnigma].SetActive(true);

        generateStraightTerrain();

        var _finishPortal = Instantiate(finishPortal, new Vector3(row-3, y+1.5f, column-column/2), Quaternion.identity);
        _finishPortal.name = "FinishPortal";

        _finishPortal.transform.SetParent(parent.transform);

    }

    public void generateStraightTerrain()
    {
        startEnigma1Z = 0;
        endEnigma1Z = column;
        startEnigma2Z = 0;
        endEnigma2Z = column;

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

    /**public void generateLShapeTerrain()
    {
        startEnigma1Z = columnLShaped - startEnigma1Z;
        endEnigma1Z = columnLShaped - endEnigma1Z;
        for (int r = startingX; r <= rowLShaped; r++)
        {
            x = r;
            for (int c = startingZ; c <= columnLShaped; c++)
            {
                z = c;
                //if ((r < startEnigma1X || r >= endEnigma1X) &&
                  /// (z < startEnigma2Z || z >= endEnigma2Z) &&
                   //(z < firstTerrainZ && r>firstTerrainX))
                //{
                if (
                    (z < columnLShaped - firstTerrainZ && x < firstTerrainX) ||
                    (r > startEnigma1X && r < endEnigma1X)

                    )
                {
                    empty = true;
                }
                else
                {
                    empty = false;
                }

                if (!empty)
                {
                    string name = "pavimento[" + r.ToString() + ";" + y.ToString() + ";" + c.ToString() + "]";
                    var cube = Instantiate(floor, new Vector3(x, y, z), Quaternion.identity);
                    cube.name = name;
                    cube.transform.SetParent(parent.transform);
                }

                if (r == rowLShaped || c == columnLShaped)
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
    }*/

    public int getStartX(string name)
    {
        if (name == enigmas[firstEnigma].name)
        {
            print("Ritornato X=" + startEnigma1X + " a " + name);
            return startEnigma1X;
        }
        else if (name == enigmas[secondEnigma].name)
        {
            print("Ritornato X=" + startEnigma2X + " a " + name);
            return startEnigma2X;
        }
        else
        {
            print("Ritornato X=0 a " + name);
            return 0;
        }
    }
    public int getEndX(string name)
    {
        if (name == enigmas[firstEnigma].name)
        {
            return endEnigma1X;
        }
        else if (name == enigmas[secondEnigma].name)
        {
            return endEnigma2X;
        }
        else
        {
            return 0;
        }
    }

    public int getStartZ(string name)
    {
        if (name == enigmas[firstEnigma].name)
        {
            return startEnigma1Z;
        }
        else if (name == enigmas[secondEnigma].name)
        {
            return startEnigma2Z;
        }
        else
        {
            return 0;
        }
    }
    public int getEndZ(string name)
    {
        if (name == enigmas[firstEnigma].name)
        {
            return endEnigma1Z;
        }
        else if (name == enigmas[secondEnigma].name)
        {
            return endEnigma2Z;
        }
        else
        {
            return 0;
        }
    }

    public int getStartY(string name)
    {
        if (name == enigmas[firstEnigma].name)
        {
            return startEnigma1Y;
        }
        else if (name == enigmas[secondEnigma].name)
        {
            return startEnigma2Y;
        }
        else
        {
            return 0;
        }
    }

    public int getEnigmaNumber(string name)
    {
        if (name == enigmas[firstEnigma].name)
        {
            return 1;
        }
        else if (name == enigmas[secondEnigma].name)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
}

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
    private int firstObstacle;
    private int secondObstacle;
    private int startingX = 0;
    private int startingZ = 0;

    [SerializeField] private int row;         
    [SerializeField] private int column;            
    [SerializeField] private int y;
    private int firstTerrainZ;
    private int firstTerrainX;

    [SerializeField] private int wallHeight;

    [SerializeField] private int startObstacle1X;     
    [SerializeField] private int endObstacle1X;       
    private int startObstacle1Z;     
    private int endObstacle1Z;       
    [SerializeField] private int startObstacle1Y;     

    [SerializeField] private int startObstacle2X;     
    [SerializeField] private int endObstacle2X;       
    private int startObstacle2Z;     
    private int endObstacle2Z;       
    [SerializeField] private int startObstacle2Y;     

    private int startLShapedObstacle2X;  
    private int endLShapedObstacle2X;    
    private int startLShapedObstacle2Z;  
    private int endLShapedObstacle2Z;    

    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject finishPortal;

    public GameObject[] obstacles;

    void Start()
    {
        int l = obstacles.Length;
        do
        {
            firstObstacle = Random.Range(0, l);
            secondObstacle = Random.Range(0, l);
        } while (firstObstacle == secondObstacle);

        obstacles[firstObstacle].SetActive(true);
        obstacles[secondObstacle].SetActive(true);

        generateStraightTerrain();

        var _finishPortal = Instantiate(finishPortal, new Vector3(row-3, y+1.5f, column-column/2), Quaternion.identity);
        _finishPortal.name = "FinishPortal";

        _finishPortal.transform.SetParent(parent.transform);

    }

    public void generateStraightTerrain()
    {
        startObstacle1Z = 0;
        endObstacle1Z = column;
        startObstacle2Z = 0;
        endObstacle2Z = column;

        for (int r = startingX; r <= row; r++)
        {
            x = r;
            if (
                ((r < startObstacle1X || r >= endObstacle1X)) &&
                ((r < startObstacle2X || r >= endObstacle2X))
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
        startObstacle1Z = columnLShaped - startObstacle1Z;
        endObstacle1Z = columnLShaped - endObstacle1Z;
        for (int r = startingX; r <= rowLShaped; r++)
        {
            x = r;
            for (int c = startingZ; c <= columnLShaped; c++)
            {
                z = c;
                //if ((r < startObstacle1X || r >= endObstacle1X) &&
                  /// (z < startObstacle2Z || z >= endObstacle2Z) &&
                   //(z < firstTerrainZ && r>firstTerrainX))
                //{
                if (
                    (z < columnLShaped - firstTerrainZ && x < firstTerrainX) ||
                    (r > startObstacle1X && r < endObstacle1X)

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
        if (name == obstacles[firstObstacle].name)
        {
            print("Ritornato X=" + startObstacle1X + " a " + name);
            return startObstacle1X;
        }
        else if (name == obstacles[secondObstacle].name)
        {
            print("Ritornato X=" + startObstacle2X + " a " + name);
            return startObstacle2X;
        }
        else
        {
            print("Ritornato X=0 a " + name);
            return 0;
        }
    }
    public int getEndX(string name)
    {
        if (name == obstacles[firstObstacle].name)
        {
            return endObstacle1X;
        }
        else if (name == obstacles[secondObstacle].name)
        {
            return endObstacle2X;
        }
        else
        {
            return 0;
        }
    }

    public int getStartZ(string name)
    {
        if (name == obstacles[firstObstacle].name)
        {
            return startObstacle1Z;
        }
        else if (name == obstacles[secondObstacle].name)
        {
            return startObstacle2Z;
        }
        else
        {
            return 0;
        }
    }
    public int getEndZ(string name)
    {
        if (name == obstacles[firstObstacle].name)
        {
            return endObstacle1Z;
        }
        else if (name == obstacles[secondObstacle].name)
        {
            return endObstacle2Z;
        }
        else
        {
            return 0;
        }
    }

    public int getStartY(string name)
    {
        if (name == obstacles[firstObstacle].name)
        {
            return startObstacle1Y;
        }
        else if (name == obstacles[secondObstacle].name)
        {
            return startObstacle2Y;
        }
        else
        {
            return 0;
        }
    }

    public int getObstacleNumber(string name)
    {
        if (name == obstacles[firstObstacle].name)
        {
            return 1;
        }
        else if (name == obstacles[secondObstacle].name)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
}

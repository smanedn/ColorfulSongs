using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainGenerator : MonoBehaviour
{

    private int x;
    private int z;
    private int h;
    private bool end;
    private bool empty;
    private int firstObstacle;
    private int secondObstacle;
    private int enemy;
    private int startingX = 0;
    private int startingZ = 0;

    [Header("Level Size")]
    [SerializeField] private int row;
    [SerializeField] private int column;
    [SerializeField] private int y;
    private int firstTerrainZ;
    private int firstTerrainX;
    [SerializeField] private int wallHeight;
    private int rowLShaped = 20;
    private int columnLShaped = 20;

    [Header("First Obstacle position")]
    [SerializeField] private int startObstacle1X;
    [SerializeField] private int endObstacle1X;
    private int startObstacle1Z;
    private int endObstacle1Z;
    [SerializeField] private int startObstacle1Y;

    [Header("Second Obstacle position")]
    [SerializeField] private int startObstacle2X;
    [SerializeField] private int endObstacle2X;
    private int startObstacle2Z;
    private int endObstacle2Z;
    [SerializeField] private int startObstacle2Y;

    private int startLShapedObstacle2X;
    private int endLShapedObstacle2X;
    private int startLShapedObstacle2Z;
    private int endLShapedObstacle2Z;

    private float enemyX;
    private float enemyZ;
    private float enemyY;

    [Header("Prefab")]
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject transparent;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject finishPortal;
    [SerializeField] private HealthManager hm;
    [SerializeField] private Score sc;
    [SerializeField] private GameObject inGameGUI;
    [SerializeField] private GameObject winGUI;
    [SerializeField] private GameObject deathGUI;
    [SerializeField] private GameObject ButtonGUI;

    public GameObject[] obstacles;
    public GameObject[] enemies;


    void Start()
    {
        if (PlayerPrefs.GetInt("LevelEnded") == 1)
        {
            generateEndRoom();
        }
        else
        {
            hm.resetInvincible();
            print("LIVELLI COMPLETATI " + PlayerPrefs.GetInt("CompletedLevels"));
            enemyX = endObstacle1X + (startObstacle2X - endObstacle1X) / 2f;
            enemyZ = column / 2 - 0.5f;
            enemyY = 1.5f;

            int l = obstacles.Length;
            int n = enemies.Length;
            do
            {
                firstObstacle = Random.Range(0, l);
                secondObstacle = Random.Range(0, l);
                enemy = Random.Range(0,n);
            } while (firstObstacle == secondObstacle);

            obstacles[firstObstacle].SetActive(true);
            obstacles[secondObstacle].SetActive(true);
            if(enemies[enemy].name != "Drum")
            {
                enemies[enemy].SetActive(true);
            }
            
            generateStraightTerrain();

            var _enemy = Instantiate(enemies[enemy], new Vector3(enemyX, enemyY, enemyZ), Quaternion.identity);
            _enemy.name = "Enemy";
            _enemy.transform.SetParent(parent.transform);
            _enemy.SetActive(true);

            var _finishPortal = Instantiate(finishPortal, new Vector3(row - 3, y + 1.5f, column - column / 2), Quaternion.identity);
            _finishPortal.name = "FinishPortal";
            _finishPortal.transform.SetParent(parent.transform);
        }
    }

    private void Update()
    {
        if (hm.IsDead() || PlayerPrefs.GetInt("CompletedLevels") > 4)
        {
            if (hm.IsDead())
            {
                print("DEAD in UPDATE");
                PlayerPrefs.SetInt("Dead", 1);
                PlayerPrefs.Save();
            }
            print("LEVEL FINITO:\nHeart: " + hm.IsDead() + "\nLivelli completati: " + PlayerPrefs.GetInt("CompletedLevels"));
            PlayerPrefs.SetInt("LevelEnded", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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

    public void generateEndRoom()
    {
        for (int r = 0; r <= 7; r++)
        {
            x = r;
            for (int c = 0; c <= 7; c++)
            {
                z = c;
                string name = "pavimento[" + r.ToString() + ";" + y.ToString() + ";" + c.ToString() + "]";
                var cube = Instantiate(floor, new Vector3(x, y, z), Quaternion.identity);
                cube.name = name;
                cube.transform.SetParent(parent.transform);

                if (r == 7 || c == 7)
                {
                    for (int a = y; a <= 5; a++)
                    {
                        string nameWall = "wall[" + r.ToString() + ";" + a.ToString() + ";" + c.ToString() + "]";
                        var wallObj = Instantiate(wall, new Vector3(x, a, z), Quaternion.identity);
                        wallObj.name = nameWall;
                        wallObj.transform.SetParent(parent.transform);
                    }
                }
                else if (r == 0 || c == 0)
                {
                    for (int a = y; a <= 5; a++)
                    {
                        string nameWall = "transparentWall[" + r.ToString() + ";" + a.ToString() + ";" + c.ToString() + "]";
                        var wallObj = Instantiate(transparent, new Vector3(x, a, z), Quaternion.identity);
                        wallObj.name = nameWall;
                        wallObj.transform.SetParent(parent.transform);
                    }
                }
            }
        }

        ButtonGUI.SetActive(true);
        sc.setGUIScore();
        inGameGUI.SetActive(false);
        GameObject.Find("Character").GetComponent<PlayerMovement>().enabled = false;
        Time.timeScale = 0f;
        if (PlayerPrefs.GetInt("CompletedLevels") >= 5)
        {
            winGUI.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Dead") == 1)
        {
            HealthManager.DeathScreen();
        }

    }

    public void generateLShapeTerrain()
    {
        startObstacle1Z = columnLShaped - startObstacle1Z;
        endObstacle1Z = columnLShaped - endObstacle1Z;
        for (int r = startingX; r <= rowLShaped; r++)
        {
            x = r;
            for (int c = startingZ; c <= columnLShaped; c++)
            {
                z = c;
                if ((r < startObstacle1X || r >= endObstacle1X) &&
                   (z < startObstacle2Z || z >= endObstacle2Z) &&
                   (z < firstTerrainZ && r > firstTerrainX))
                {
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
        }
    }

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
        public int getStartX2() { return startObstacle2X; }
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

        public int getEndX1() { return endObstacle1X; }

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

        public int getEndZ()
        {
            return endObstacle1Z;

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

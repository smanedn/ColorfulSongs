using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonsGenerator : MonoBehaviour
{
    private int x;
    private int z;
    private int h;
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
    [SerializeField] private GameObject ButtonGUI;

    public GameObject[] enemies;

    void Start()
    {
        if (PlayerPrefs.GetInt("LevelEnded") == 1 && PlayerPrefs.GetInt("roomGenerated") == 0)
        {
            generateEndRoom();
            PlayerPrefs.SetInt("roomGenerated", 1);
            PlayerPrefs.Save();
        }
        else
        {
            hm.resetInvincible();

            /*int n = enemies.Length;

            if(enemies[enemy].name != "Drum")
            {
                enemies[enemy].SetActive(true);
            }*/
            
            generateRooms();

            /*var _enemy = Instantiate(enemies[enemy], new Vector3(enemyX, enemyY, enemyZ), Quaternion.identity);
            _enemy.name = "Enemy";
            _enemy.transform.SetParent(parent.transform);
            _enemy.SetActive(true);
            */
        }
    }

    private void Update()
    {
        if ( hm.IsDead() || PlayerPrefs.GetInt("CompletedLevels") > 4 && PlayerPrefs.GetInt("roomGenerated") == 0)
        {
            if (hm.IsDead())
            {
                PlayerPrefs.SetInt("Dead", 1);
            }
            PlayerPrefs.SetInt("LevelEnded", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void generateRooms()
    {
        int roomToGenerate = 5;
        int startingR = 0;
        int originalRow = row;

        for(int rc = 0; rc<roomToGenerate; rc++){
            startingR = startingX + (originalRow * rc);
            for (int r = startingR; r <= row; r++)
            {
                x = r;
                for (int c = startingZ; c <= column; c++)
                {
                    z = c;
                    string name = "Floor[" + r.ToString() + ";" + y.ToString() + ";" + c.ToString() + "]";
                    var cube = Instantiate(floor, new Vector3(x, y, z), Quaternion.identity);
                    cube.name = name;
                    cube.transform.SetParent(parent.transform);


                    if (r == row || c == column)
                    {
                        for (int a = y; a <= wallHeight; a++)
                        {
                            if(r==row && c == column - column / 2){
                            }
                            else{
                                string nameWall = "Wall[" + r.ToString() + ";" + a.ToString() + ";" + c.ToString() + "]";
                                var wallObj = Instantiate(wall, new Vector3(x, a, z), Quaternion.identity);
                                wallObj.name = nameWall;
                                wallObj.transform.SetParent(parent.transform);
                            }
                            
                        }
                    }
                }
            }
            row+=originalRow;
        }

        var _finishPortal = Instantiate(finishPortal, new Vector3(row - (originalRow+3), y + 1.5f, column - column / 2), Quaternion.identity);
        _finishPortal.name = "FinishPortal";
        _finishPortal.transform.SetParent(parent.transform);
    }

    public void generateEndRoom()
    {
        if (PlayerPrefs.GetInt("roomGenerated") == 0) 
        {
            for (int r = 0; r <= 7; r++)
            {
                x = r;
                for (int c = 0; c <= 7; c++)
                {
                    z = c;
                    string name = "floor[" + r.ToString() + ";" + y.ToString() + ";" + c.ToString() + "]";
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
                HealthManager.EndScreen("Won!");
            }
            else if (PlayerPrefs.GetInt("Dead") == 1)
            {
                HealthManager.EndScreen("Lost");
            }
        }
    }
}

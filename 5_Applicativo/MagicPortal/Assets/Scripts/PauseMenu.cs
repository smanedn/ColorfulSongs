using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] private GameObject inGameGUI;
    [SerializeField] private GameObject winGUI;
    [SerializeField] private static bool isPaused;
    [SerializeField] private AudioSource musicSource;

    private HealthManager healthManager;
    private string currentInput;

    void Start()
    {
        healthManager = new HealthManager();
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        inGameGUI.SetActive(true);
        Cursor.visible = false;
        if (!PlayerPrefs.HasKey("CompletedLevels"))
        {
            PlayerPrefs.SetInt("CompletedLevels", 0);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetInt("CompletedLevels") >= 5)
        {
            winGUI.SetActive(true);
            PlayerPrefs.SetInt("CompletedLevels", 0);
            PlayerPrefs.Save();
        }
        levelText.text = string.Format("Level: {0}", (PlayerPrefs.GetInt("CompletedLevels")));
        Debug.Log("Completed Levels: " + PlayerPrefs.GetInt("CompletedLevels"));
    }

    // Update is called once per frame
    void Update()
    {
        print("dead: " + healthManager.IsDead());
        if ((Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.JoystickButton7)) && !healthManager.IsDead())
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (isPaused)
        {
           Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }

    public void PauseGame()
    {
        pauseMenu?.SetActive(true);
        inGameGUI.SetActive(false);
        Time.timeScale = 0f;
        musicSource?.Pause();
        isPaused = !isPaused;
    }

    public void ResumeGame()
    {
        pauseMenu?.SetActive(false);
        inGameGUI.SetActive(true);
        Time.timeScale = 1f;
        musicSource?.UnPause(); 
        isPaused = !isPaused;
    }

    public void Restart()
    {
        if (healthManager.IsDead())
        {
            PlayerPrefs.SetInt("CompletedLevels", 0);
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}

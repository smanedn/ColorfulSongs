using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] private GameObject inGameGUI;
    [SerializeField] private static bool isPaused;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private Score sc;

    private HealthManager healthManager;
    private string currentInput;
    public TextMeshProUGUI[] winTime;

    void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        inGameGUI.SetActive(true);
        Cursor.visible = false;
        if (!PlayerPrefs.HasKey("CompletedLevels"))
        {
            PlayerPrefs.SetInt("CompletedLevels", 0);
            PlayerPrefs.Save();
        }
        levelText.text = string.Format("Level: {0}", PlayerPrefs.GetInt("CompletedLevels"));
    }

    void Update()
    {
        if ((Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.JoystickButton7)) && PlayerPrefs.GetInt("LevelEnded")==0)
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
        inGameGUI.SetActive(true);
        Time.timeScale = 1f;
        pauseMenu?.SetActive(false);
        musicSource?.UnPause();
        isPaused = !isPaused;
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("CompletedLevels", 0);
        PlayerPrefs.SetInt("LevelEnded", 0);
        PlayerPrefs.SetInt("roomGenerated", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("LevelEnded", 0);
        PlayerPrefs.SetInt("roomGenerated", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Main Menu");
    }
}

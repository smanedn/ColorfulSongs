using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject inGameGUI;
    [SerializeField] private static bool isPaused;
    [SerializeField] private AudioSource musicSource;

    void Start()
    {
        pauseMenu.SetActive(false);
        inGameGUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
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

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}

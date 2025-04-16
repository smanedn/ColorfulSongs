using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject inGameGUI;
    [SerializeField] private static bool isPaused;
    [SerializeField] private AudioSource musicSource;

    private string currentInput;

    void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        inGameGUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current != null && Mouse.current.delta.ReadValue().magnitude > 0)
        {
            currentInput = "MK";
        }

        float rightStickHorizontal = Input.GetAxis("Horizontal");
        float rightStickVertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(rightStickHorizontal) > 0.1f || Mathf.Abs(rightStickVertical) > 0.1f)
        {
            currentInput = "PAD";
        }

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.JoystickButton7))
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
            if (currentInput == "MK")
            {
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
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

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}

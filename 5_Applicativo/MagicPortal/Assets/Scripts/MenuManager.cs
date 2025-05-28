using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("CompletedLevels"))
        {
            PlayerPrefs.SetInt("LevelEnded", 0);
            PlayerPrefs.SetInt("CompletedLevels", 0);
            PlayerPrefs.SetInt("roomGenerated", 0);
            PlayerPrefs.Save();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

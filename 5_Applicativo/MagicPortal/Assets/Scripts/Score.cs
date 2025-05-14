using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private float scoreValue;
    private int hundredths;
    private int seconds;
    private int minutes;
    private string time;
    [SerializeField]  private GameObject scoreParent;
    public TextMeshProUGUI[] scores;


    void Start()
    {
        scoreValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreValue < 300)  //5 minutes = 300 seconds
        {
            scoreValue += Time.deltaTime;
        }
        //else
        //{
        //    HealthManager.DeathScreen();
        //}

        hundredths = Mathf.FloorToInt(scoreValue * 100) % 100;
        seconds = Mathf.FloorToInt(scoreValue % 60);
        minutes = Mathf.FloorToInt(scoreValue / 60);
        setScore(minutes, seconds, hundredths);
    }

    public string getScore()
    {
        return time;
    }

    public void setScore(int m, int s, int h)
    { 
        scoreText.text = string.Format("{0:00}:{1:00}:{2:00}", m, s, h);
        time = scoreText.text;
    }

    public void setGUIScore()
    {
        scoreParent.SetActive(true);
        for (int i = 0; i < PlayerPrefs.GetInt("CompletedLevels"); i++)
        {
            string name = "Time" + i;
            int j = i + 1;
            scores[i].text = string.Format("Score {0}: " + PlayerPrefs.GetString(name), j);
        }
        for(int i = PlayerPrefs.GetInt("CompletedLevels");i<5; i++)
        {
            int j = i + 1;
            //scores[i].text = string.Format("{0}" + PlayerPrefs.GetString(name), j);
            scores[i].text = string.Format("");
        }
    }
}

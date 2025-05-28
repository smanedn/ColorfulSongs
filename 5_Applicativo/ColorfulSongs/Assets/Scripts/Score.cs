using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private float scoreValue;
    private int hundredths;
    private int seconds;
    private int minutes;
    private string time;
    [SerializeField]  private GameObject scoreParent;
    [SerializeField] private API_CALL api;
    public TextMeshProUGUI[] scores;
    public TextMeshProUGUI finalScore;

    void Start()
    {
        scoreValue = 0f;
    }

    void Update()
    {
        if (scoreValue < 300)  //5 minutes = 300 seconds
        {
            scoreValue += Time.deltaTime;
        }

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
        scoreText.text = string.Format("{0:00}:{1:00}.{2:00}", m, s, h);
        time = scoreText.text;
    }

    public void setGUIScore()
    {
        TimeSpan timeSum = new TimeSpan(0, 0, 0, 0, 0);
        scoreParent.SetActive(true);
        for (int i = 0; i < PlayerPrefs.GetInt("CompletedLevels"); i++)
        {

            string name = "Time" + i;
            int j = i + 1;
            TimeSpan timeForSum = TimeSpan.Parse("00:00:" + PlayerPrefs.GetString(name));
            timeSum += timeForSum;
            scores[i].text = string.Format("Score {0}: " + PlayerPrefs.GetString(name), j);
        }
        for(int i = PlayerPrefs.GetInt("CompletedLevels");i<5; i++)
        {
            int j = i + 1;
            scores[i].text = string.Format("");
        }
        string finalTime  = timeSum.ToString();
        finalTime = finalTime.Remove(0, 3);
        print(finalTime.Length);
        finalScore.text = string.Format("Final score: " + finalTime);
        PlayerPrefs.SetString("score", finalTime);
        if (PlayerPrefs.GetInt("CompletedLevels") > 4)
        {
            StartCoroutine(api.addScore());
        }
        
    }
}

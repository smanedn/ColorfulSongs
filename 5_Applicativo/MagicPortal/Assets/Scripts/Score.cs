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
        else
        {
            HealthManager.DeathScreen();
        }

        hundredths = Mathf.FloorToInt(scoreValue * 100) % 100;
        seconds = Mathf.FloorToInt(scoreValue % 60);
        minutes = Mathf.FloorToInt(scoreValue / 60);
        scoreText.text = string.Format("{0:00}:{1:00}:{2:00}",minutes,seconds,hundredths);
    }
}

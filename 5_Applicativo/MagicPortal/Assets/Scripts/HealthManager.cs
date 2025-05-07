using System.Collections;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; } // Singleton
    [SerializeField] private GameObject healthBar;
    private static int health; // 0-5
    private static bool isInvincible;
    private float invincibilityDurationSeconds = 2;
    private Transform[] heartImages;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private GameObject inGameGUI;
    [SerializeField] private GameObject deathGUI;
    [SerializeField] private GameObject hitGUI; 
    private AudioManager audioManager;
    [SerializeField] private Score sc;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Evita duplicati nel caso di più scene
        }
    }

    void Start()
    {
        heartImages = healthBar.GetComponentsInChildren<Transform>();
        print(heartImages.Length);
        health = heartImages.Length - 1;

        deathGUI.gameObject.SetActive(false);
    }

    public void LooseOneHeart()
    {

        
        if (!isInvincible)
        {
            string currentLastHeart = "Heart " + health;
            GameObject.Find(currentLastHeart).SetActive(false);
            audioManager.PlaySFX(audioManager.GetHit());
            health -= 1;
            
            if (IsDead())
            {
                //print("dead");
                DeathScreen();
                sc.setGUIScore();
            }
            else
            {
                hitGUI.SetActive(true);
                StartCoroutine(SetInvincible());
            }
        }
    }
    private IEnumerator SetInvincible()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDurationSeconds);

        isInvincible = false;
        hitGUI.SetActive(false);

    }



    public static void DeathScreen()
    {
        Time.timeScale = 0f;
        Instance.musicSource?.Pause();
        Instance.deathGUI.gameObject?.SetActive(true);
        //Instance.inGameGUI.gameObject?.SetActive(false);
        GameObject.Find("Character").GetComponent<PlayerMovement>().enabled = false;
        Cursor.visible = true;
    }

    public static int GetHealth()
    {
        return health;
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}

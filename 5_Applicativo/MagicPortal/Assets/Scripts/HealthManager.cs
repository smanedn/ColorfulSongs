using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static HealthManager Instance { get; private set; } // Singleton


    [SerializeField] private GameObject healthBar;
    public static int health; // 0-5
    private Transform[] heartImages;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private GameObject deathGUI;

    void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LooseOneHeart()
    {
        string currentLastHeart = "Heart " + health;
        GameObject.Find(currentLastHeart).SetActive(false);
    }

    public static void DeathScreen()
    {
       Time.timeScale = 0f;
       Instance.musicSource?.Pause();
       Instance.deathGUI.gameObject?.SetActive(true);
    }
}

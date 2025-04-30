
using System.Collections;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; } // Singleton
    [SerializeField] private GameObject healthBar;
    private static int health; // 0-5
    private Transform[] heartImages;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private GameObject deathGUI;
    [SerializeField] private GameObject hitGUI;

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

    public static void LooseOneHeart()
    {

        string currentLastHeart = "Heart " + health;
        GameObject.Find(currentLastHeart).SetActive(false);
        health -= 1;
        if (IsDead())
        {
            print("dead");
            DeathScreen();
        }
        Instance.hitGUI.GetComponent<HitGUI>().showHitGUI();
    }


    public static void DeathScreen()
    {
        Time.timeScale = 0f;
        Instance.musicSource?.Pause();
        Instance.deathGUI.gameObject?.SetActive(true);
        GameObject.Find("Character").GetComponent<PlayerMovement>().enabled = false;
        Cursor.visible = true;
    }

    public static int GetHealth()
    {
        return health;
    }

    public static bool IsDead()
    {
        return health <= 0;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}

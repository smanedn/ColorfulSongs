using System.Collections;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    //HealthManager healthManager = new HealthManager();
    private HealthManager healthManager;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private Score sc;
   

    void Start()
    {
        healthManager = HealthManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
                //print("hit");
                healthManager.LooseOneHeart();
        }
        if (other.gameObject.CompareTag("FinishPortal"))
        {
            int CompletedLevels = PlayerPrefs.GetInt("CompletedLevels");
            string timeToComplete = sc.getScore();
            string timeForLevel = "Time" + CompletedLevels;
            CompletedLevels++;
            print(CompletedLevels);
            PlayerPrefs.SetInt("CompletedLevels", CompletedLevels);
            PlayerPrefs.SetString(timeForLevel, timeToComplete);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Game");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            int x = portal.GetComponent<PortalGenerator>().getEndX();
            int z = portal.GetComponent<PortalGenerator>().getEndZ();
            //print("tp");
            Teleport(x+1, 0.75f, z, false);
        }

        if (other.gameObject.CompareTag("BadPortal"))
        {
            Teleport(1, 0.75f, 1, true);
        }

        
    }

    public async void Teleport(float x, float y, float z, bool damage)
    {
        GetComponent<PlayerMovement>().enabled = false;
        await Task.Delay(300);
        transform.position = new Vector3(x, y, z);
        if (damage)
        {
            healthManager.LooseOneHeart();
        }
        await Task.Delay(300);
        GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}

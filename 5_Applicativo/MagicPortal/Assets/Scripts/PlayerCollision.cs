using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerCollision : MonoBehaviour
{
    private static bool isInvincible;
    [SerializeField] private GameObject portal;
    //HealthManager hm = new HealthManager();
    private HealthManager hm;
    [SerializeField] private PauseMenu pauseMenu;

    void Start()
    {
        hm = HealthManager.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //if (!isInvincible)
            //{
                print("hit");
                hm.LooseOneHeart();
                //isInvincible = true;
               // SetInvincible();
            //}
            //else
            //{
                //print("Invincibile");
            //}

        }
    }

    public void SetInvincible()
    {
        StartCoroutine(Wait(5f));
        isInvincible = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            int x = portal.GetComponent<PortalGenerator>().getEndX();
            int z = portal.GetComponent<PortalGenerator>().getEndZ();
            print("tp");
            Teleport(x+1, 0.75f, z, false);
        }

        if (other.gameObject.CompareTag("BadPortal"))
        {
            Teleport(1, 0.75f, 1, true);
        }

        if (other.gameObject.CompareTag("FinishPortal"))
        {
            int CompletedLevels = PlayerPrefs.GetInt("CompletedLevels");
            CompletedLevels++;
            PlayerPrefs.SetInt("CompletedLevels", CompletedLevels);
            PlayerPrefs.Save();
            print(pauseMenu);
            pauseMenu.Restart();
        }
    }

    public async void Teleport(float x, float y, float z, bool damage)
    {
        GetComponent<PlayerMovement>().enabled = false;
        await Task.Delay(300);
        transform.position = new Vector3(x, y, z);
        if (damage)
        {
            hm.LooseOneHeart();
        }
        await Task.Delay(300);
        GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}

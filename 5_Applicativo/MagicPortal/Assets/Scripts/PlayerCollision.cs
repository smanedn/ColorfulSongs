using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject hitGUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("hit");
            //Destroy(other.gameObject);
            HealthManager.LooseOneHeart();
            hitGUI.SetActive(true);
            print(HealthManager.GetHealth());
            StartCoroutine(Wait(0.5f));
            hitGUI.SetActive(false);
        }
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
            PauseMenu.Restart();
        }
    }

    public async void Teleport(float x, float y, float z, bool damage)
    {
        GetComponent<PlayerMovement>().enabled = false;
        await Task.Delay(300);
        transform.position = new Vector3(x, y, z);
        if (damage)
        {
            HealthManager.LooseOneHeart();
        }
        await Task.Delay(300);
        GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}

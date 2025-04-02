using System.Threading.Tasks;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
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
            print(HealthManager.GetHealth());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            print("tp");
            Teleport(0, 2f, 0);
        }

        if (other.gameObject.CompareTag("BadPortal"))
        {
            print("badPortal");
            Teleport(0, 2f, 0);
            HealthManager.LooseOneHeart();
        }
    }

    public async void Teleport(float x, float y, float z)
    {
        GetComponent<PlayerMovement>().enabled = false;
        await Task.Delay(300);
        transform.position = new Vector3(x, y, z);
        await Task.Delay(300);
        GetComponent<PlayerMovement>().enabled = true;
    }
}

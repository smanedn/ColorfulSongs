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
        if (other.gameObject.tag == "Enemy")
        {
            print("hit");
            //Destroy(other.gameObject);
            HealthManager.LooseOneHeart();
            HealthManager.health -= 1;
            print(HealthManager.health);
            if (HealthManager.health == 0)
            {
                print("dead");
                HealthManager.DeathScreen();
            }
        }
    }
}

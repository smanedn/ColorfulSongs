using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            Destroy(this.gameObject);
            if(this.name == "CannonBall(Clone)")
            {
                HealthManager.LooseOneHeart();
                print(HealthManager.GetHealth());
            }
            if (this.name == "BadPortal")
            {
                HealthManager.LooseOneHeart();
                print(HealthManager.GetHealth());
            }
        }
        


    }
}

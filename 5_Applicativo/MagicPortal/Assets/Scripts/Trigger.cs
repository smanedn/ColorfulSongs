using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        if(this.name == "CannonBall(Clone)")
        {
            HealthManager.LooseOneHeart();
            print(HealthManager.GetHealth());
        }
        
        
    }
}

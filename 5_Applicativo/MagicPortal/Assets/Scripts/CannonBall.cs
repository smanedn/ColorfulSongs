using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject); 
        HealthManager hm = HealthManager.Instance;
        hm.LooseOneHeart();
        print(HealthManager.GetHealth());
    }
}

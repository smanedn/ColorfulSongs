using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        HealthManager.LooseOneHeart();
        print(HealthManager.GetHealth());
    }
}

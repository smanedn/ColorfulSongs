using UnityEngine;

public class Trigger : MonoBehaviour
{
    HealthManager hm = HealthManager.Instance;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            Destroy(this.gameObject);
            if(this.name == "CannonBall(Clone)")
            {
                hm.LooseOneHeart();
            }
            if (this.name == "BadPortal")
            {
                hm.LooseOneHeart();
            }
        }
    }
}

using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public float speed = 5.0f;
    [SerializeField] private Rigidbody rb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 position = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;

        transform.Translate(position);
    }
}

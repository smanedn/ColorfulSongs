using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] public GameObject player;
    private Vector3 offset;
    private float y;
    void Start()
    {
        offset = transform.position - player.transform.position;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}


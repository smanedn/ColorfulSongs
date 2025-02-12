using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 1f;
    private CharacterController characterController;
    private float veritcalVelocity;
    private float x;
    private float y;
    private float z;
    private Vector3 position;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        Movement();
        Turn();
    }
    private void Movement()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        y = VerticalForceCalculation();
       
        position = new Vector3(x + z, y, z - x);
        characterController.Move(position * speed * Time.deltaTime);
    }

    private void Turn()
    {
        
        if (Mathf.Abs(position.x) > 0.1 || Mathf.Abs(position.z) > 0.1)
        {
            var targetAngle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0); 
        }
    }
    private float VerticalForceCalculation()
    {
        if (characterController.isGrounded)
        {
            veritcalVelocity = -1f;
            if (Input.GetButtonDown("Jump"))
            {
                veritcalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
            }
        }
        else
        {
            veritcalVelocity -= gravity * Time.deltaTime;
        }
        return veritcalVelocity;
    }
}

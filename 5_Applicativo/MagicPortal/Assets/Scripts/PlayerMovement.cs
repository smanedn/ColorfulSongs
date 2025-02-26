using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float runningSpeed = 12.0f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 0.5f;
    private CharacterController characterController;
    private float veritcalVelocity;
    private float x;
    private float y;
    private float z;
    private Vector3 position;

    AudioManagerGame audioManager;

    private void Awake()
    {
        audioManager = GetComponent<AudioManagerGame>();
    }

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
        /*if(y == -1f)
        {
            position = position.normalized;
        }*/
        
        Debug.Log(position);
        Debug.Log(Time.deltaTime);

        // Determina la velocit�
        float currentSpeed = speed;

        
        if (Input.GetButton("Fire3") && (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f))
        {
            currentSpeed = runningSpeed;

        }

        characterController.Move(position * currentSpeed * Time.deltaTime);    //in caso non si voglia + speed in diagonale positino.normalized
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
                //audioManager.PlaySFX(audioManager.GetJump());
            }
        }
        else
        {
            veritcalVelocity -= gravity * Time.deltaTime;
        }
        return veritcalVelocity;
    }
}

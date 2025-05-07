using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float runningSpeed = 6.5f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 0.5f;
    private AudioManager audioManager;
    private CharacterController characterController;
    private float veritcalVelocity;
    private float x;
    private float y;
    private float z;
    private Vector3 position;
    private float voidHeight = -15;
    private int defaultMovement;
    private float initialRotationY;
    private HealthManager healthManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        healthManager = new HealthManager();
        characterController = GetComponent<CharacterController>();
        if (!PlayerPrefs.HasKey("DefaultMovement"))
        {
            PlayerPrefs.SetInt("DefaultMovement", 1);
            Debug.Log("c");
            PlayerPrefs.Save();
        }
        defaultMovement = PlayerPrefs.GetInt("DefaultMovement");
        Debug.Log("Mov: " + defaultMovement);

        if (defaultMovement == 0)
        {
            initialRotationY = 0f;
        }
        else if (defaultMovement == 1)
        {
            initialRotationY = -45f;
        }

        transform.rotation = Quaternion.Euler(0, initialRotationY, 0);
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
        
        if (x > 0f || z > 0f || x < 0f || z < 0f)
        {
            GetComponent<Animator>().SetBool("isWalking", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isWalking", false);
        }
        y = VerticalForceCalculation();
       
        if(defaultMovement == 0)
        {
            position = new Vector3(x + z, y, z - x);
        }
        else if (defaultMovement == 1)
        {
            position = new Vector3(x, y, z);
        }
        
        /*if(y == -1f)
        {
            position = position.normalized;
        }*/
        
        //Debug.Log(position);
        //Debug.Log(Time.deltaTime);

        // Determina la velocità
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
            transform.rotation = Quaternion.Euler(0, initialRotationY + targetAngle, 0);
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
                audioManager.PlaySFX(audioManager.GetJump());
            }

            //MoveArm("down");
        }
        else
        {
            veritcalVelocity -= gravity * Time.deltaTime;

            //MoveArm("up");
            if (IsVoid())
            {
                print("Void");
                if (!healthManager.IsDead())
                {
                    GetComponent<PlayerCollision>().Teleport(0, 2f, 0, true);
                }
            }
        }
        return veritcalVelocity;
    }
    private bool IsVoid()
    {
        if (transform.position.y < voidHeight)
        {
            return true;
        }
        return false;
    }
}

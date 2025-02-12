using UnityEngine;
using UnityEngine.InputSystem;

public class ProvaPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float runningSpeed = 6.0f;  // Aggiungi la velocità per la corsa
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 0.5f;
    private CharacterController characterController;
    private float veritcalVelocity;
    private float x;
    private float y;
    private float z;
    private Vector3 position;


    private bool isWalking;
    private bool isRunning;
    private Animator animator;  // Riferimento all'animatore per le animazioni

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();  // Ottieni il componente Animator
    }

    void Update()
    {
        Movement();
        Turn();
        UpdateAnimationStates();  // Controlla e aggiorna le animazioni
        
    }

    private void Movement()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        y = VerticalForceCalculation();


        // Determina la velocità
        float currentSpeed = speed;

        // Se il giocatore sta tenendo premuto "Shift" per correre, aumenta la velocità
        if (Input.GetButton("Fire3") && (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f))
        {
            currentSpeed = runningSpeed;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // Se il giocatore si sta muovendo ma non sta correndo, è in camminata
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        position = new Vector3(x + z, y, z - x);
        characterController.Move(position * currentSpeed * Time.deltaTime);
    }

    private void Turn()
    {
        // La rotazione del player in base alla direzione del movimento
        if (Mathf.Abs(position.x) > 0.1f || Mathf.Abs(position.z) > 0.1f)
        {
            var targetAngle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
    }

    private float VerticalForceCalculation()
    {
        if (characterController.isGrounded)
        {
            veritcalVelocity = -1f;  // Leggera spinta verso il basso per tenere il giocatore ancorato al suolo
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

    // Gestisce i cambiamenti di stato per le animazioni
    private void UpdateAnimationStates()
    {
        // Usa l'animatore per modificare lo stato dell'animazione in base a isWalking e isRunning
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
    }
}

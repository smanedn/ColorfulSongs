using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    CharacterController crct;
    public float x;
    public float z;
    public float jumpHeight = 2f;
    public float playerSpeed = 5f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        crct = GetComponent<CharacterController>();
    }

    void Update()
    {

        isGrounded = crct.isGrounded;
        Debug.Log(isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f; // Reset velocity.y quando è a terra
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        long vel = 1;

        // Calcola la direzione del movimento
        var dir = new Vector3(x + z, 0, z - x);

        if (dir.sqrMagnitude > 0.1)
        {
            // Calcola l'angolo di rotazione del personaggio in base ai movimenti
            var targetAngle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            // Ruota il personaggio sull'asse Y verso la direzione in cui si sta muovendo
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            crct.Move(dir * vel * Time.deltaTime);
        }

        // Gestione del salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Applica la gravità
        velocity.y += gravity * Time.deltaTime;

        // Muovi il CharacterController in base alla gravità
        crct.Move(velocity * Time.deltaTime);
        //Debug.Log(isJump);

    }
}
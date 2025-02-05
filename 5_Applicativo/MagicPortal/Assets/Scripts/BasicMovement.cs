using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    CharacterController crct;
    public float x;
    public float z;
    public float speed;
    public float gravity;
    public float jump;
    // Start is called before the first frame update
    void Start()
    {
        crct = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        long vel = 1;

        //Quando premo W/S aumenta sia la x che la z
        var dir = new Vector3(x+z, gravity, z-x);

        if (dir.sqrMagnitude != 0)
        {
            //calcola l'angolo di rotazione del personaggio in base ai movimenti, poi cambia da rad2deg
            var targetAngle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            //ruota il personaggio sull'asse Y verso la direzione in cui si sta muovendo
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            crct.Move(dir * vel * Time.deltaTime);
        }
    }
}

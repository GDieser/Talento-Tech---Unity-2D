using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMov : MonoBehaviour
{
    //Direccion
    private int movHorizontal = 0;
    private int movVertical = 0;
    private Vector2 mov = new Vector2(0, 0);
    private bool timerOn;
    public float tiempoSprint = 3;
    public float tiempoSEspera = 0;
    public Light2D linterna;


    //Velocidad
    [SerializeField] private float speed;

    //Acceder a sus prop
    private Rigidbody2D rb;

    void Start()
    {
        //Optiene los componente del rb
        linterna.enabled = true;
        rb = GetComponent<Rigidbody2D>();
        timerOn = true;
    }

    void Update()
    {
        LookAtMouse();
        MovY();
        MovX();
        LinternaOff();

        if (!Sprint())
        {
            RecargarSprint();
        }
        

        mov = new Vector2(movHorizontal, movVertical);
        //Normaliza la velocida para que no se sumen
        mov = mov.normalized;

        //Evitar el movimiento limitado por fps, lo limitamos por tiempo
        //transform.Translate(mov * speed * Time.deltaTime);



    }

    //Para manejar fisicas
    private void FixedUpdate()
    {
        //Empuja el obj (para movimientos)
        //rb.AddForce(mov * speed * Time.deltaTime);
        rb.velocity = (mov * speed * Time.fixedDeltaTime);
        //rb.velocity = mov * speed;
    }

    private void LookAtMouse()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = (Vector3)(mousePos - new Vector2(transform.position.x, transform.position.y));
        
    }

    private void LinternaOff()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            linterna.enabled = !linterna.enabled;
        }        

    }

    private bool Sprint()
    {
        speed = 30;

        if (Input.GetKey(KeyCode.LeftShift) && 
            (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) 
            && timerOn)
        {
            if (tiempoSprint > 0)
            {
                tiempoSprint -= Time.deltaTime;
            }
            else
            {
                timerOn = false;
                return false;
            }
            speed = 45;
        }
        else if(!timerOn)
        {
            speed = 30;
            return false;
        }
        return true;

    }

    private void RecargarSprint()
    {
        if (tiempoSEspera <= 5 && timerOn == false)
        {
            tiempoSEspera += Time.deltaTime;
        }
        else if (tiempoSEspera > 5)
        {
            timerOn = true;
            tiempoSEspera = 0;
            tiempoSprint = 3;
        }
    }

    private void MovY()
    {
        //Eje Y
        if (Input.GetKey(KeyCode.W))
        {
            movVertical = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movVertical = -1;
        }
        else
        {
            movVertical = 0;
        }
    }

    private void MovX()
    {
        //Eje X
        if (Input.GetKey(KeyCode.D))
        {
            movHorizontal = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movHorizontal = -1;
        }
        else
        {
            movHorizontal = 0;
        }
    }

}

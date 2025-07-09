using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static MenuPause;

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

    [SerializeField] private AudioClip flashLight;
    private Animator animator;


    //Velocidad
    private float speed;
    [SerializeField] private float speedCons = 30;

    //Acceder a sus prop
    private Rigidbody2D rb;

    void Start()
    {
        //Optiene los componente del rb
        transform.position = GameState.startPosition;
        linterna.enabled = true;
        rb = GetComponent<Rigidbody2D>();
        timerOn = true;
        animator = GetComponentInChildren<Animator>();

        PlayerShotRifle rifle = GetComponent<PlayerShotRifle>();
        rifle.active = true;

    }

    void Update()
    {
        MovY();
        MovX();
        LinternaOff();
        AttackMelee();
        ChangeGun();

        if (!Sprint())
        {
            RecargarSprint();
        }
        

        mov = new Vector2(movHorizontal, movVertical);
        //Normaliza la velocida para que no se sumen
        mov = mov.normalized;

        //Evitar el movimiento limitado por fps, lo limitamos por tiempo
        //transform.Translate(mov * speed * Time.deltaTime);

        bool estaCaminando = movHorizontal != 0 || movVertical != 0;
        animator.SetBool("isWalking", estaCaminando);

    }

    //Para manejar fisicas
    private void FixedUpdate()
    {
        //Empuja el obj (para movimientos)
        //rb.AddForce(mov * speed * Time.deltaTime);
        rb.velocity = (mov * speed * Time.fixedDeltaTime);
        //rb.velocity = mov * speed;
    }

    public void ChangeGun(int op = 0)
    {
        PlayerShotRevolver revolver = GetComponent<PlayerShotRevolver>();
        PlayerShotRifle rifle = GetComponent<PlayerShotRifle>();
        PlayerShotShotGun shotGun = GetComponent<PlayerShotShotGun>();

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetKeyDown(KeyCode.Alpha1) || op == 1)
        {
            if(rifle.active || shotGun.GetActive())
            {
                rifle.enabled = false;
                shotGun.enabled = false;

                revolver.enabled = true;
                animator.CrossFade("Idle", 0f);
                animator.SetInteger("Select_Gun", 0);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || op == 2)
        {
            if ((revolver.enabled == true) || (shotGun.GetActive()) && rifle.active == true)
            {
                revolver.enabled = false;
                shotGun.enabled = false;

                rifle.enabled = true;

                animator.CrossFade("Rifle_Idle", 0f);
                animator.SetInteger("Select_Gun", 1);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || op == 3)
        {
            if (((rifle.enabled == true) || (revolver.enabled == true))&& shotGun.GetActive())
            {
                rifle.enabled = false;
                revolver.enabled = false;

                shotGun.enabled = true;

                animator.CrossFade("ShotGun_Idle", 0f);
                animator.SetInteger("Select_Gun", 2);
            }

        }
    }

    private void AttackMelee()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Melee");
        }
    }

    private void LinternaOff()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            linterna.enabled = !linterna.enabled;
            SoundController.instance.PlaySound(flashLight, 0.8f);
        }        

    }

    private bool Sprint()
    {
        speed = speedCons;

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
            speed = speedCons * 1.5f;
        }
        else if(!timerOn)
        {
            speed = speedCons;
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

    /*
    private void MovY()
    {
        //Eje Y
        if (Input.GetKey(KeyCode.W))
        {
            movVertical = 1;
            animator.SetTrigger("Walk");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movVertical = -1;
            animator.SetTrigger("Walk");
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
            animator.SetTrigger("Walk");
            
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movHorizontal = -1;
            animator.SetTrigger("Walk");
        }
        else
        {
            
            movHorizontal = 0;
        }
    }*/

    private void MovY()
    {
        if (Input.GetKey(KeyCode.W))
            movVertical = 1;
        else if (Input.GetKey(KeyCode.S))
            movVertical = -1;
        else
            movVertical = 0;
    }

    private void MovX()
    {
        if (Input.GetKey(KeyCode.D))
            movHorizontal = 1;
        else if (Input.GetKey(KeyCode.A))
            movHorizontal = -1;
        else
            movHorizontal = 0;
    }

}

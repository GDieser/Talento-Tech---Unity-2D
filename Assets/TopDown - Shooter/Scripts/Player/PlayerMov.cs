using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
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

    [SerializeField] private Image revolverImage;
    [SerializeField] private Image revolverBullet;

    [SerializeField] private Image rifleImage;
    [SerializeField] private Image rifleBullet;

    [SerializeField] private Image shotGunImage;
    [SerializeField] private Image shotGunBullet;

    [SerializeField] private bool isLevel1 = true;

    // Armas disponibles (desbloqueo)
    public bool hasRevolver = true;
    public bool hasRifle = false;
    public bool hasShotgun = false;

    // Armas activas
    private PlayerShotRevolver revolver;
    private PlayerShotRifle rifle;
    private PlayerShotShotGun shotGun;

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

        revolver = GetComponent<PlayerShotRevolver>();
        rifle = GetComponent<PlayerShotRifle>();
        shotGun = GetComponent<PlayerShotShotGun>();

        // Por defecto solo tiene revólver
        hasRevolver = true;
        hasRifle = !isLevel1; // si es lvl2 o superior, tiene rifle
        hasShotgun = false; // desbloqueás más adelante

        // Activar revólver al inicio
        ActivarRevolver();

        //revolverImage.enabled = true;
        //revolverBullet.enabled = true;

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

        //rb.velocity = mov * speed;
        if (mov == Vector2.zero)
            rb.velocity = Vector2.zero;

    }

    /*
    public void ChangeGun(int op = 0)
    {
        PlayerShotRevolver revolver = GetComponent<PlayerShotRevolver>();
        PlayerShotRifle rifle = GetComponent<PlayerShotRifle>();
        PlayerShotShotGun shotGun = GetComponent<PlayerShotShotGun>();

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetKeyDown(KeyCode.Alpha1) || op == 1)
        {
            if(rifle.active || (shotGun != null && shotGun.GetActive()))
            {
                rifle.enabled = false;
                if (shotGun != null)
                    shotGun.enabled = false;

                rifleImage.enabled = false;
                rifleBullet.enabled = false;
                shotGunImage.enabled = false;   
                shotGunBullet.enabled = false;

                revolverImage.enabled = true;
                revolverBullet.enabled = true;

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
                
                if(shotGun != null)
                    shotGun.enabled = false;

                revolverImage.enabled = false;
                revolverBullet.enabled = false;
                shotGunImage.enabled = false;
                shotGunBullet.enabled = false;

                rifleImage.enabled = true;
                rifleBullet.enabled = true;
                

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

                revolverImage.enabled = false;
                revolverBullet.enabled = false;
                rifleImage.enabled = false;
                rifleBullet.enabled = false;

                shotGunImage.enabled = true;
                shotGunBullet.enabled = true;

                shotGun.enabled = true;

                animator.CrossFade("ShotGun_Idle", 0f);
                animator.SetInteger("Select_Gun", 2);
            }

        }
    }
    */

    public void ChangeGun(int op = 0)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || op == 1)
        {
            if (hasRevolver) ActivarRevolver();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || op == 2)
        {
            if (hasRifle) ActivarRifle();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || op == 3)
        {
            if (hasShotgun) ActivarShotgun();
        }
    }

    private void ActivarRevolver()
    {
        DesactivarTodas();
        revolver.enabled = true;

        revolverImage.enabled = true;
        revolverBullet.enabled = true;

        animator.CrossFade("Idle", 0f);
        animator.SetInteger("Select_Gun", 0);
    }

    private void ActivarRifle()
    {
        DesactivarTodas();
        rifle.enabled = true;

        rifleImage.enabled = true;
        rifleBullet.enabled = true;

        animator.CrossFade("Rifle_Idle", 0f);
        animator.SetInteger("Select_Gun", 1);
    }

    private void ActivarShotgun()
    {
        DesactivarTodas();
        shotGun.enabled = true;

        shotGunImage.enabled = true;
        shotGunBullet.enabled = true;

        animator.CrossFade("ShotGun_Idle", 0f);
        animator.SetInteger("Select_Gun", 2);
    }

    private void DesactivarTodas()
    {
        if (revolver != null) revolver.enabled = false;
        if (rifle != null) rifle.enabled = false;
        if (shotGun != null) shotGun.enabled = false;

        revolverImage.enabled = false;
        revolverBullet.enabled = false;
        rifleImage.enabled = false;
        rifleBullet.enabled = false;
        shotGunImage.enabled = false;
        shotGunBullet.enabled = false;
    }

    public void DesbloquearRifle()
    {
        hasRifle = true;
    }

    public void DesbloquearShotgun()
    {
        hasShotgun = true;
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

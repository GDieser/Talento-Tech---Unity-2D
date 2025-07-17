using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static MenuPause;
using static PlayerVida;
using UnityEngine.SceneManagement;
using static PlayerMission;

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

    private bool[] availableWeapons;
    private int currentWeaponIndex = 0;


    // Armas activas
    private PlayerShotRevolver revolver;
    private PlayerShotRifle rifle;
    private PlayerShotShotGun shotGun;

    private PlayerVida vida;
    private PlayerMission mission;

    private Rigidbody2D rb;

    public float tiempoSinUsarSprint = 0f; 
    private float tiempoEsperaRecarga = 2f;
    private bool estaRecargando = false;

    void Start()
    {


        transform.position = GameState.startPosition;
        linterna.enabled = true;
        rb = GetComponent<Rigidbody2D>();
        timerOn = true;
        animator = GetComponentInChildren<Animator>();

        revolver = GetComponent<PlayerShotRevolver>();
        rifle = GetComponent<PlayerShotRifle>();
        shotGun = GetComponent<PlayerShotShotGun>();


        hasRevolver = true;
        hasRifle = !isLevel1;
        hasShotgun = GameManager.instance.shotGun;
        //hasShotgun = true;

        ActivarRevolver();
        availableWeapons = new bool[] { hasRevolver, hasRifle, hasShotgun };

        if (!isLevel1)
        {
            //Debug.Log("Guardado en GameManager: " + GameManager.instance.totalRevolverBullets);

            if(GameManager.instance.totalRifleBullets != 0)
            {
                
                rifle.totalBullets = GameManager.instance.totalRifleBullets;
                revolver.totalBullets = GameManager.instance.totalRevolverBullets;


                if (GameManager.instance.shotGun)
                {
                    hasShotgun = true;
                    shotGun.SetTotalBase(GameManager.instance.totalShotGunBullets);

                }

                vida = GetComponent<PlayerVida>();

                vida.AddPack(GameManager.instance.totalPacks);

                GameStateStory.hablo = GameManager.instance.hablo;
                GameStateStory.sirena1 = GameManager.instance.sirena1;
                GameStateStory.sirena2 = GameManager.instance.sirena2;
            }
            else
            {
                rifle.totalBullets = 30;
                revolver.totalBullets = 16;
            }

            
            //mission = GetComponent<PlayerMission>();

            //mission.AgregarItems();

        }

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

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            if (scroll > 0) 
            {
                SwitchWeapon(1); 
            }
            else 
            {
                SwitchWeapon(-1);
            }
        }

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
        if (Input.GetKeyDown(KeyCode.Alpha1)) op = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) op = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) op = 3;

        switch (op)
        {
            case 1:
                if (hasRevolver) ActivarRevolver();
                currentWeaponIndex = 0;
                break;
            case 2:
                if (hasRifle) ActivarRifle();
                currentWeaponIndex = 1;
                break;
            case 3:
                if (hasShotgun) ActivarShotgun();
                currentWeaponIndex = 2;
                break;
        }
    }

    private void SwitchWeapon(int direction)
    {
        // Guardar el índice inicial para evitar bucles infinitos
        int startIndex = currentWeaponIndex;
        bool weaponFound = false;

        do
        {
            // Calcular nuevo índice
            currentWeaponIndex += direction;

            // Asegurarse de que estamos dentro de los límites del array
            if (currentWeaponIndex >= availableWeapons.Length)
                currentWeaponIndex = 0;
            else if (currentWeaponIndex < 0)
                currentWeaponIndex = availableWeapons.Length - 1;

            // Si el arma está disponible, activarla
            if (availableWeapons[currentWeaponIndex])
            {
                weaponFound = true;
                switch (currentWeaponIndex)
                {
                    case 0: ActivarRevolver(); break;
                    case 1: ActivarRifle(); break;
                    case 2: ActivarShotgun(); break;
                }
            }

            // Si hemos dado la vuelta completa sin encontrar arma disponible, salir
            if (currentWeaponIndex == startIndex)
                break;

        } while (!weaponFound);
    }

    /*
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
    */
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
        availableWeapons[1] = true;
        hasRifle = true;
    }

    public void DesbloquearShotgun()
    {
        availableWeapons[2] = true;
        hasShotgun = true;
    }

    private void AttackMelee()
    {
        if (Input.GetButtonDown("Fire2"))
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

    /*
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
        else if (!timerOn)
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
    */

    private bool Sprint()
    {
        bool estaSprintando = Input.GetKey(KeyCode.LeftShift) &&
                              (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                              Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));

        // Si está sprintando y tiene energía, reducir tiempoSprint
        if (estaSprintando && timerOn)
        {
            tiempoSinUsarSprint = 0f; // Reiniciar contador de inactividad
            estaRecargando = false;

            if (tiempoSprint > 0)
            {
                tiempoSprint -= Time.deltaTime;
                speed = speedCons * 1.5f;
                return true;
            }
            else
            {
                timerOn = false;
                speed = speedCons;
                return false;
            }
        }
        else
        {
            // Si no está sprintando, aumentar tiempoSinUsarSprint
            tiempoSinUsarSprint += Time.deltaTime;

            // Si pasó el tiempo de espera, empezar a recargar
            if (tiempoSinUsarSprint >= tiempoEsperaRecarga)
            {
                estaRecargando = true;
                RecargarSprint();
            }

            speed = speedCons;
            return false;
        }
    }

    private void RecargarSprint()
    {
        if (estaRecargando && tiempoSprint < 3f) // 3f = tiempo máximo de sprint
        {
            tiempoSprint += Time.deltaTime * 2f; // Velocidad de recarga (ajustable)

            // Si se llena completamente, reiniciar estado
            if (tiempoSprint >= 3f)
            {
                tiempoSprint = 3f;
                timerOn = true;
                estaRecargando = false;
            }
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

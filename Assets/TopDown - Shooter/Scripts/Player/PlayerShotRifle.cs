using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerShotRifle : MonoBehaviour
{
    [SerializeField] public bool active = false;

    [SerializeField] private Transform controlShoot;
    [SerializeField] private GameObject bullet;

    [SerializeField] public int bullets = 30;
    [SerializeField] public int totalBullets = 0;
    public int MaxBullets = 240;

    [SerializeField] private float coolDown = 0.2f;
    private float currentCoolDown = 0f;

    [SerializeField] private TotalBullets total;

    [SerializeField] private AudioClip audioShoot;
    [SerializeField] private AudioClip audioReload;
    [SerializeField] private AudioClip audioReloadClick;
    [SerializeField] private AudioClip audioNoBullets;

    [SerializeField] private Animator ShotLight;

    private Animator animator;

    private void Start()
    {
        //audioSource = this.GetComponent<AudioSource>();
        if (animator == null)
        {
            //Me direcciona al nimator del player
            animator = GetComponentInChildren<Animator>();
        }
    }
    private void Update()
    {
        total.setBullets(bullets);
        total.setTotalBullets(totalBullets);
        CoolDown();
        ClickShoot();
        ReloadGun();

    }

    public void ClickShoot()
    {
        if (Input.GetButton("Fire1") && bullets > 0 && currentCoolDown == 0)
        {
            //Acá Dispara
            Shoot();
            SoundController.instance.PlaySound(audioShoot, 0.5f);
            animator.SetTrigger("Shoot");

        }
        else if (Input.GetButtonDown("Fire1") && currentCoolDown == 0)
        {
            SoundController.instance.PlaySound(audioNoBullets, 0.8f);
            animator.SetTrigger("Shoot");
        }


    }

    public void ReloadGun()
    {
        if (Input.GetKeyDown(KeyCode.R) || (bullets == 0 && totalBullets > 0))
        {
            if (totalBullets > 0 && bullets < 30)
            {
                ReloadBulletsGun();
                SoundController.instance.PlaySound(audioReload, 0.4f);
                animator.SetTrigger("Reload");
            }
            else
            {
                SoundController.instance.PlaySound(audioReloadClick, 0.4f);

            }
        }

    }

    public void AddBullet(int bullets)
    {
        if(totalBullets < MaxBullets)
        {
            totalBullets += bullets;
        }

        if (totalBullets > 240)
            totalBullets = 240;

    }

    public void ReloadBulletsGun()
    {
        while (bullets < 30 && totalBullets > 0)
        {
            bullets++;
            totalBullets--;
        }
    }

    private void Shoot()
    {

        GameObject gb = Instantiate(bullet, controlShoot.position, controlShoot.rotation);

        if (gb != null)
        {
            Destroy(gb.gameObject, 1.5f);
        }

        ShotLight.CrossFade("Shoot", 0f);
        ShotLight.SetTrigger("Shoot");
        bullets--;
        currentCoolDown = coolDown;
    }

    private void CoolDown()
    {
        if (currentCoolDown > 0)
        {
            currentCoolDown -= Time.deltaTime;
            if (currentCoolDown < 0)
                currentCoolDown = 0;
        }

    }
}

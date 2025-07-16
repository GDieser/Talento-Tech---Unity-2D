using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotRevolver : MonoBehaviour
{
    [SerializeField] public bool active = true;
    [SerializeField] private Transform controlShoot;
    [SerializeField] private GameObject bullet;

    [SerializeField] public int bullets = 8;
    [SerializeField] public int totalBullets = 10;
    public int MaxBullets = 48;

    [SerializeField] private float coolDown = 0.5f;
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
        if (Input.GetButtonDown("Fire1") && bullets > 0 && currentCoolDown == 0)
        {
            //Acá Dispara
            Shoot();
            ShotLight.SetTrigger("Shoot");


            SoundController.instance.PlaySound2(audioShoot, 0.2f);

            animator.SetTrigger("Shoot");

        }
        else if(Input.GetButtonDown("Fire1") && currentCoolDown == 0)
        {
            //ShotLight.SetActive(true);
            SoundController.instance.PlaySound(audioNoBullets, 0.8f);
            animator.SetTrigger("Shoot");
        }

        //ShotLight.SetActive(false);
    }

    public void ReloadGun()
    {
        if (Input.GetKeyDown(KeyCode.R) || (bullets == 0 && totalBullets > 0))
        {
            if(totalBullets > 0 && bullets < 8)
            {
                ReloadBulletsGun();
                SoundController.instance.PlaySound(audioReload, 0.4f);
                animator.SetTrigger("Reload");
            }
            else
            {
                SoundController.instance.PlaySound(audioReloadClick, 1f);
                
            }
        }

    }

    public int GetTotalBullets()
    {
        return totalBullets;
    }

    public void AddBullet(int bullets)
    {
        if (totalBullets < MaxBullets)
        {
            totalBullets += bullets;
        }

        if (totalBullets > 48)
            totalBullets = 48;
    }

    public void ReloadBulletsGun()
    {
        while (bullets < 8 && totalBullets > 0)
        {
            bullets++;
            totalBullets--;
        }
    }

    private void Shoot()
    {

        GameObject gb = Instantiate(bullet, controlShoot.position, controlShoot.rotation);

        if(gb != null)
        {
            Destroy(gb.gameObject, 1f);
        }

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

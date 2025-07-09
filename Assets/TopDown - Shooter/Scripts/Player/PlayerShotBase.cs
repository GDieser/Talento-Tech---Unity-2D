using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotBase : MonoBehaviour
{
    [SerializeField] protected bool active = false;

    [SerializeField] protected Transform controlShoot;
    [SerializeField] protected GameObject bullet;

    [SerializeField] protected int bullets;
    [SerializeField] protected int totalBullets;
    protected int MaxBullets;

    [SerializeField] protected float coolDown;
    protected float currentCoolDown = 0f;

    [SerializeField] protected TotalBullets total;

    [SerializeField] protected AudioClip audioShoot;
    [SerializeField] protected AudioClip audioReload;
    [SerializeField] protected AudioClip audioReloadClick;
    [SerializeField] protected AudioClip audioNoBullets;
    protected int bulletReload;
    protected int bulletMax;

    [SerializeField] protected Animator ShotLight;

    protected Animator animator;
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //total.setBullets(bullets);
        //total.setTotalBullets(totalBullets);
        CoolDown();
        ClickShoot();
        ReloadGun(bulletReload, bulletMax);
    }

    protected virtual void ClickShoot()
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

    public bool GetActive()
    {
        return active;
    }

    public int GetTotalBullets()
    {
        return totalBullets;
    }

    protected virtual void ReloadGun(int bullet, int maxBullets)
    {
        if (Input.GetKeyDown(KeyCode.R) || (bullets == 0 && totalBullets > 0))
        {
            if (totalBullets > 0 && bullets < maxBullets)
            {
                ReloadBulletsGun(bullet);
                SoundController.instance.PlaySound(audioReload, 0.4f);
                animator.SetTrigger("Reload");
            }
            else
            {
                SoundController.instance.PlaySound(audioReloadClick, 0.4f);

            }
        }

    }

    public virtual void AddBullet(int bullets, int maxBullets)
    {
        totalBullets += bullets;
    }

    protected void ReloadBulletsGun(int total)
    {
        while (bullets < total && totalBullets > 0)
        {
            bullets++;
            totalBullets--;
        }
    }

    protected void Shoot(int cantidad = 1)
    {

        for (int i = 0; i < cantidad; i++)
        {
            float spreadAngle = Random.Range(-7f, 7f);

            Quaternion spreadRotation = controlShoot.rotation * Quaternion.Euler(0f, 0f, spreadAngle);

            Instantiate(bullet, controlShoot.position, spreadRotation);
        }
        
        ShotLight.CrossFade("Shoot", 0f);
        ShotLight.SetTrigger("Shoot");
        bullets--;
        currentCoolDown = coolDown;
    }

    protected void CoolDown()
    {
        if (currentCoolDown > 0)
        {
            currentCoolDown -= Time.deltaTime;
            if (currentCoolDown < 0)
                currentCoolDown = 0;
        }

    }
}

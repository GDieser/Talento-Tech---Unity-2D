using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerShotShotGun : PlayerShotBase
{
    [SerializeField] AudioClip PreReloadSound;

    protected float timer = 0;
    private bool isReloading = false;
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        bullets = 6;
        totalBullets = 0;
        MaxBullets = 32;
        coolDown = 0.8f;
        bulletReload = 6;
        bulletMax = 6;
    }

    // Update is called once per frame
    void Update()
    {
        total.setBullets(bullets);
        total.setTotalBullets(totalBullets);

        CoolDown();
        ClickShoot();
        ReloadGun(bulletReload, bulletMax);
    }

    public int GetTotalBullets()
    {
        return totalBullets;
    }

    public void SetActive(bool setActive)
    {
        active = setActive;
    }


    protected override void ClickShoot()
    {
        if (Input.GetButton("Fire1") && bullets > 0 && currentCoolDown == 0)
        {
            Shoot(10);
            SoundController.instance.PlaySound(audioShoot, 0.5f);
            SoundController.instance.PlaySound(audioReload, 0.5f);
            animator.SetTrigger("Shoot");

        }
        else if (Input.GetButtonDown("Fire1") && currentCoolDown == 0)
        {
            SoundController.instance.PlaySound(audioNoBullets, 0.8f);
            animator.SetTrigger("Shoot");
        }


    }

    protected override void ReloadGun(int bullet, int maxBullets)
    {
        if ((Input.GetKeyDown(KeyCode.R) || (bullets == 0 && totalBullets > 0)) && !isReloading)
        {
            if (totalBullets > 0 && bullets < maxBullets)
            {
                StartCoroutine(ReloadShellByShell(bullet, maxBullets));
            }
            else
            {
                SoundController.instance.PlaySound(audioReloadClick, 0.4f);
            }
        }

    }

    public override void AddBullet(int bullets, int maxBullets)
    {
        if (totalBullets < MaxBullets)
        {
            totalBullets += bullets;
        }

        if (totalBullets > maxBullets)
            totalBullets = maxBullets;

    }

    private IEnumerator ReloadShellByShell(int bullet, int maxBullets)
    {
        isReloading = true;
        
        yield return new WaitForSeconds(0.4f);/// ---------> importante para timers

        while (bullets < maxBullets && totalBullets > 0)
        {
            bullets++;
            totalBullets--;

            ReloadBulletsGun(bullets);
            SoundController.instance.PlaySound(PreReloadSound, 0.8f);
            
            animator.SetTrigger("Reload");

            yield return new WaitForSeconds(0.6f);

            
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
            {
                break;
            }
        }
        
        isReloading = false;
    }

    private bool Timer(float totalTime)
    {

        if (timer < totalTime)
        {
            timer += Time.deltaTime;

            return false;
        }
        else
        {
            timer = 0;
            return true;
        }
    }

}

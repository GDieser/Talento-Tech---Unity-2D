using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform controlShoot;
    [SerializeField] private GameObject bullet;

    [SerializeField] public int bullets = 4;
    [SerializeField] public int totalBullets = 0;

    [SerializeField] private float coolDown = 0.5f;
    private float currentCoolDown = 0f;

    [SerializeField] private TotalBullets total;

    private void Start()
    {
        
    }
    private void Update()
    {
        total.setBullets(bullets);
        total.setTotalBullets(totalBullets);
        CoolDown();

        if (Input.GetButtonDown("Fire1") && bullets > 0 && currentCoolDown == 0)
        {
            //Acá Dispara
            Shot();
            
        }
        if (Input.GetKeyDown(KeyCode.R) && totalBullets > 0)
            ReloadBullets();
    }

    public void AddBullet()
    {
        totalBullets += 8;
    }

    public void ReloadBullets()
    {
        while (bullets < 8 && totalBullets > 0)
        {
            bullets++;
            totalBullets--;
        }
    }

    private void Shot()
    {
        Instantiate(bullet, controlShoot.position, controlShoot.rotation);
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

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;



public class PickUpBullets : MonoBehaviour
{
    private TotalBullets total;

    [SerializeField] private bool revolver;
    [SerializeField] private bool rifle;
    [SerializeField] private bool shotGun;

    [SerializeField] private AudioClip audioBullets;

    private void Start()
    {
        //pickUp = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //pickUp.Play();

        if (collision.gameObject.CompareTag("Player"))
        {
            if(revolver)
            {
                PlayerShotRevolver bullets = collision.GetComponent<PlayerShotRevolver>();
                if (bullets != null)
                {
                    SoundController.instance.PlaySound(audioBullets, 0.8f);
                    bullets.AddBullet(8);

                    gameObject.SetActive(false);
                }
            }
            else if (rifle)
            {
                PlayerShotRifle bullets = collision.GetComponent<PlayerShotRifle>();
                if (bullets != null && bullets.totalBullets < 240)
                {
                    //Debug.Log("Carga");
                    SoundController.instance.PlaySound(audioBullets, 0.8f);
                    bullets.AddBullet(30);

                    gameObject.SetActive(false);
                }
            }
            else if(shotGun)
            {
                PlayerShotShotGun bulletsShotGun = collision.GetComponent<PlayerShotShotGun>();
                
                if(bulletsShotGun.GetActive())
                {
                    if (bulletsShotGun != null && bulletsShotGun.GetTotalBullets() < 32)
                    {
                        SoundController.instance.PlaySound(audioBullets, 0.8f);
                        bulletsShotGun.AddBullet(6, 32);

                        gameObject.SetActive(false);
                    }
                }
            }
            
        }
    }
}

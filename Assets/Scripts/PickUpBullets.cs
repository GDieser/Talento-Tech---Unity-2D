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
                    bullets.AddBullet();

                    gameObject.SetActive(false);
                }
            }
            else if (rifle)
            {
                PlayerShotRevolver bullets = collision.GetComponent<PlayerShotRevolver>();
                if (bullets != null)
                {
                    SoundController.instance.PlaySound(audioBullets, 0.8f);
                    bullets.AddBullet();

                    gameObject.SetActive(false);
                }
            }
            
        }
    }
}

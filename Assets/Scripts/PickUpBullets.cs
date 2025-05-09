using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;



public class PickUpBullets : MonoBehaviour
{
    private TotalBullets total;

    [SerializeField] private AudioClip audio;

    private void Start()
    {
        //pickUp = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //pickUp.Play();

        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerShoot bullets = collision.GetComponent<PlayerShoot>();
            if (bullets != null)
            {
                SoundController.instance.PlaySound(audio, 0.8f);
                bullets.AddBullet();

                gameObject.SetActive(false);
            }
        }
    }
}

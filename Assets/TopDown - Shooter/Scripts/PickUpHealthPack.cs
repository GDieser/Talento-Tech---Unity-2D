using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealthPack : MonoBehaviour
{
    [SerializeField] private AudioClip audioHealth;
    [SerializeField] private AudioClip audioHealth2;
    private int totalPack = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerVida vida = collision.GetComponent<PlayerVida>();

            totalPack = vida.totalPacks;

            if (vida != null && vida.vida < 8)
            {
                vida.AddHealth();
                SoundController.instance.PlaySound(audioHealth, 0.8f);
                gameObject.SetActive(false);
            }
            else if (totalPack < 4)
            {
                vida.AddHealthPack();
                totalPack++;

                SoundController.instance.PlaySound(audioHealth2,1f);
                gameObject.SetActive(false);

            }
        }
    }
}

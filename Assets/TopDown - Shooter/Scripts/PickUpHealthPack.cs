using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealthPack : MonoBehaviour
{
    [SerializeField] private AudioClip audioHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerVida vida = collision.GetComponent<PlayerVida>();
            if (vida != null && vida.vida < 8)
            {
                vida.AddHead();
                SoundController.instance.PlaySound(audioHealth, 0.8f);
                gameObject.SetActive(false);
            }
        }
    }
}

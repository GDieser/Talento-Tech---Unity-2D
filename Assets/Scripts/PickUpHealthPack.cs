using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealthPack : MonoBehaviour
{
    //[SerializeField] private PlayerVida vidaTotal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerVida vida = collision.GetComponent<PlayerVida>();
            if (vida != null && vida.vida < 4)
            {
                vida.AddHead();

                gameObject.SetActive(false);
            }
        }
    }
}

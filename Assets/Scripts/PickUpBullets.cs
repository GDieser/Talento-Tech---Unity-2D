using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



public class PickUpBullets : MonoBehaviour
{
    private TotalBullets total;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerShoot bullets = collision.GetComponent<PlayerShoot>();
            if (bullets != null)
            {
                bullets.reloadBullet();

                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("El objeto Player no tiene el componente PlayerShoot.");
            }
        }
    }
}

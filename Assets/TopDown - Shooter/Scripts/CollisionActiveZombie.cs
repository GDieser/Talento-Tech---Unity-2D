using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionActiveZombie : MonoBehaviour
{
    [SerializeField] private Zombie2Script zombie;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Player"))
        {
            zombie.AlertZombie();
        }
    }
}

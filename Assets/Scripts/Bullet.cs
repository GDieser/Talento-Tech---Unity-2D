using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private int damage;


    private void Update()
    {
        transform.Translate(Vector2.up * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Zombie"))
        {
            collision.GetComponent<ZombieScript>().Damage(damage);
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Building"))
        {
            
            Destroy(gameObject);
        }
    }
}

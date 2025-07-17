using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private int damage;
    [SerializeField] private bool IsShotGun = false;


    private void Update()
    {
        transform.Translate(Vector2.up * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag.ToString() + "Hola");

        if (collision.CompareTag("Zombie"))
        {
            if(IsShotGun)
            {
                collision.GetComponent<ZombieScript>().Damage(damage, true);
            }
            else
            {
                collision.GetComponent<ZombieScript>().Damage(damage);
            }
            
            
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Zombie2"))
        {
            collision.GetComponent<Zombie2Script>().Damage(damage);

            Destroy(gameObject);
        }
        else if (collision.CompareTag("ZombieSpawn"))
        {
            collision.GetComponent<ZombieScriptSpawn>().Damage(damage);
            
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Building"))
        {
            //Debug.Log("Acá 2");
            Destroy(gameObject);
        }


        if(IsShotGun)
        {
            Destroy(gameObject, 1f);
        }
        else
        {
            Destroy(gameObject, 3f);
        }
    }
}

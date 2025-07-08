using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Aseprite;
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
        //Debug.Log(collision.tag.ToString() + "Hola");

        if (collision.CompareTag("Zombie"))
        {
            collision.GetComponent<ZombieScript>().Damage(damage);
            
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
    }
}

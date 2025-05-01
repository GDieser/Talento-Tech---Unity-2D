using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ZombieScript : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator Animator;

    private PlayerVida vida;
    [SerializeField] private int life = 2;

    //private bool playerVivo;

    public float detectionRange = 2f; // Distancia para detectar al jugador


    private void Start()
    {
        
        //playerVivo = true;
        rb = this.GetComponent<Rigidbody2D>();
        Animator = this.GetComponent<Animator>();
    }
    private void Update()
    {
        //Para cambiar la animacion
        Animator.SetBool("Walk", movement.magnitude > 0.01f);


        //Para que siga al personaje
        Vector3 direction = player.position - transform.position;
        float distanceToPlayer = direction.magnitude;

        if (distanceToPlayer <= detectionRange)
        {
            direction.Normalize();
            movement = direction;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
        else
        {
            movement = Vector2.zero;
        }


        /*
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;*/


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            //Vector2 direccionDamage = new Vector2(transform.position.x, transform.position.y);
            Debug.Log("DAÑOOO");
            PlayerVida vida = collision.gameObject.GetComponent<PlayerVida>();
            vida.vida--;

            //vida.RecibeDamage( 1);
            //playerVivo = !vida.muerto;
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    public void Damage(int damage)
    {
        life -= damage;
        if(life <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}

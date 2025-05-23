using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScriptSpawn : MonoBehaviour
{
    public Transform player;


    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float detectionRange = 2f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator Animator;

    private PlayerVida vida;

    [SerializeField] private int life = 2;
    [SerializeField] private ParticleSystem particulas;
    [SerializeField] private bool IsTest;

    private bool enMov;
    private bool isAttacking = false;
    private bool isColliding = false;





    private void Start()
    {

        rb = this.GetComponent<Rigidbody2D>();
        Animator = this.GetComponent<Animator>();


    }
    private void Update()
    {

        if (isAttacking) return;

        //Para que siga al personaje
        Vector3 direction = player.position - transform.position;
        float distanceToPlayer = direction.magnitude;

        if (distanceToPlayer <= detectionRange)
        {
            direction.Normalize();
            movement = direction;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            enMov = true;
        }
        else
        {
            movement = Vector2.zero;
            enMov = false;
        }

        Animator.SetBool("Walk", enMov);

    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            //Vector2 direccionDamage = new Vector2(transform.position.x, transform.position.y);
            //Debug.Log("DAÑOOO");

            isAttacking = true;
            //attack = true;
            Animator.SetBool("Attack", true);

            PlayerVida vida = collision.gameObject.GetComponent<PlayerVida>();
            vida.vida--;

            StartCoroutine(FinAtaque());

            //vida.RecibeDamage( 1);
            //playerVivo = !vida.muerto;
        }

    }
    

    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = false;
            // No se cambia Animator aquí, lo maneja la Coroutine
        }
    }
    


    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColliding = true;

            if (!isAttacking)
            {
                isAttacking = true;
                Animator.SetBool("Attack", true);

                PlayerVida vida = collision.gameObject.GetComponent<PlayerVida>();
                vida.vida--;

                StartCoroutine(FinAtaque());
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Melee"))
        {

        }
    }

    
    private IEnumerator FinAtaque()
    {
        // Espera a que termine la animación actual de ataque
        yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length);

        isAttacking = false;

        if (!isColliding)
        {
            Animator.SetBool("Attack", false);
            Animator.SetBool("Walk", true); // Vuelve a caminar si no sigue colisionando
        }
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
            moveCharacter(movement);
    }

    public void Damage(int damage)
    {
        life -= damage;
        particulas.Play();
        if (life <= 0)
        {
            StopAllCoroutines(); // Por si hay otras corrutinas de ataque o movimiento activas

            Animator.Play("Death"); // Forzar animación inmediatamente

            if (!IsTest)
            {
                //Animator.SetBool("Idle", false);
                Animator.SetBool("Walk", false);
                Animator.SetBool("Attack", false);
                StartCoroutine(Morir());
            }
            else
            {
                Debug.Log("Aca");

                Destroy(gameObject);

            }
        }
    }

    private IEnumerator Morir()
    {
        Animator.SetBool("Death", true);

        // Desactivar comportamiento del enemigo, pero no su GameObject
        rb.simulated = false; // Desactiva física
        GetComponent<Collider2D>().enabled = false; // Desactiva colisión
        this.enabled = false; // Desactiva este script
        // Espera que termine la animación de muerte
        yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length);



        // El personaje queda en el último frame de la animación de muerte
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
    
}

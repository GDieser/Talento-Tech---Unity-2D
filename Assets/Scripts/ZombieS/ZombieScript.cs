using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;

public class ZombieScript : MonoBehaviour
{
    public Transform player;


    private float moveSpeed = 0.5f;
    private float detectionRange = 2f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator Animator;

    private PlayerVida vida;

    [SerializeField] private int life = 2;
    [SerializeField] private ParticleSystem particulas;
    [SerializeField] private bool IsTest;

    //SpawnZombies spawnZombies = FindObjectOfType<SpawnZombies>();


    private bool enMov;
    //private bool attack;
    private bool isAttacking = false;
    private bool isColliding = false;
    //private bool death;

    [SerializeField] private AudioClip Alert1;
    [SerializeField] private AudioClip Alert2;
    [SerializeField] private AudioClip Alert3;

    [SerializeField] private AudioClip Attack1;
    [SerializeField] private AudioClip Attack2;
    [SerializeField] private AudioClip Attack3;

    [SerializeField] private AudioClip Impact1;
    [SerializeField] private AudioClip Impact2;
    [SerializeField] private AudioClip Impact3;

    private AudioSource audio;

    private bool IsAlert = false;

    private System.Random random = new System.Random();

    private void Start()
    {

        //playerVivo = true;
        rb = this.GetComponent<Rigidbody2D>();
        Animator = this.GetComponent<Animator>();


    }
    private void Update()
    {
        //Para cambiar la animacion
        //Animator.SetBool("Walk", movement.magnitude > 0.01f);

        if (isAttacking) return;

        //Para que siga al personaje
        Vector3 direction = player.position - transform.position;
        float distanceToPlayer = direction.magnitude;

        if (distanceToPlayer <= detectionRange)
        {
            if(!IsAlert)
            {
                int rand = random.Next(1, 4);

                if (rand == 1)
                    SoundController.instance.PlaySound(Alert1, 0.8f);
                else if (rand == 2)
                    SoundController.instance.PlaySound(Alert2, 0.8f);
                else if (rand == 3)
                    SoundController.instance.PlaySound(Alert3, 0.8f);

                IsAlert = true;
            }

            if((audio = GetComponent<AudioSource>()) != null )
            {
                audio.enabled = true;
            }

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
            int rand = random.Next(1, 6);

            if (rand == 1)
                SoundController.instance.PlaySound(Attack1, 0.8f);
            else if (rand == 2)
                SoundController.instance.PlaySound(Attack2, 0.8f);
            else if (rand == 3)
                SoundController.instance.PlaySound(Attack3, 0.8f);

            isAttacking = true;
            Animator.SetBool("Attack", true);

            PlayerVida vida = collision.gameObject.GetComponent<PlayerVida>();
            vida.vida--;

            StartCoroutine(FinAtaque());
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
        int rand = random.Next(1, 6);

        if (rand == 1)
            SoundController.instance.PlaySound(Impact1, 0.8f);
        else if (rand == 2)
            SoundController.instance.PlaySound(Impact2, 0.8f);
        else if (rand == 3)
            SoundController.instance.PlaySound(Impact3, 0.8f);

        life -= damage;
        particulas.Play();
        if (life <= 0)
        {
            StopAllCoroutines();

            Animator.Play("Death");

            if (!IsTest)
            {
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

        rb.simulated = false; 
        GetComponent<Collider2D>().enabled = false; 
        this.enabled = false; 

        yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length);

    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}

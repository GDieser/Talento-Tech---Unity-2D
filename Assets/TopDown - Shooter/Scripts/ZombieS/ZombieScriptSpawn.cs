using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class ZombieScriptSpawn : MonoBehaviour
{
    public Transform player;

    [SerializeField] private float moveSpeed = 1.2f;
    [SerializeField] private float detectionRange = 10f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator Animator;

    private PlayerVida vida;

    [SerializeField] private int life = 1;
    [SerializeField] private ParticleSystem particulas;
    [SerializeField] private bool IsTest;

    private bool enMov;
    private bool isAttacking = false;
    private bool isColliding = false;

    [SerializeField] private GameObject HealthPack;
    [SerializeField] private GameObject Bullets;

    private float timer = 0;

    private System.Random random = new System.Random();

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
        yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length);

        isAttacking = false;

        if (!isColliding)
        {
            Animator.SetBool("Attack", false);
            Animator.SetBool("Walk", true);
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
            int rand = random.Next(1,11);

            if(rand == 1 || rand == 2)
                Bullets = Instantiate(Bullets, transform.position, Quaternion.identity);
            else if(rand == 3)
                HealthPack = Instantiate(HealthPack, transform.position, Quaternion.identity);

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

        yield return new WaitForSeconds(5f);

        Destroy(gameObject);

    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
    
}

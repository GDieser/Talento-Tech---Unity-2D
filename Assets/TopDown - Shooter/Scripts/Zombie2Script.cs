using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie2Script : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;

    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float detectionRange = 2f;
    [SerializeField] BoxCollider2D boxCollider;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator Animator;

    private PlayerVida vida;

    [SerializeField] private int life = 2;
    //[SerializeField] private ParticleSystem particulas;
    [SerializeField] private bool IsScream = false;
    private bool Alert = false;
    private bool isWaiting = false;


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
        //capsuleCollider = this.GetComponent<CapsuleCollider2D>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }
    private void Update()
    {

        if (isAttacking) return;

        float distanceToPlayer = 0f;

        //Para que siga al personaje
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            distanceToPlayer = direction.magnitude;

            if (distanceToPlayer <= detectionRange || Alert)
            {
                agent.SetDestination(player.position);

                //agent.speed = moveSpeed;

                if (!IsAlert)
                {
                    if (IsScream)
                    {
                        AlertAndPause();
                    }
                    else
                    {
                        int rand = random.Next(1, 3);

                        if (rand == 1)
                            SoundController.instance.PlaySound(Alert2, 0.8f);
                        else if (rand == 2)
                            SoundController.instance.PlaySound(Alert3, 0.8f);
                    }


                    IsAlert = true;
                }

                if ((audio = GetComponent<AudioSource>()) != null)
                {
                    audio.enabled = true;
                }


                Vector3 moveDir = agent.velocity;

                if (moveDir.sqrMagnitude > 0.01f) // Para evitar que rote cuando está quieto
                {
                    float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
                    rb.rotation = angle;
                }

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
    }

    public void AlertZombie()
    {
        if (IsScream)
        {
            Alert = true;
            boxCollider.enabled = false;

        }
    }

    public void AlertAndPause()
    {
        if (!isWaiting)
            StartCoroutine(AlertPauseCoroutine());
    }


    private IEnumerator AlertPauseCoroutine()
    {
        isWaiting = true;

        SoundController.instance.PlaySound(Alert1, 0.8f);
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        agent.isStopped = false;

        isWaiting = false;
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
            vida.RecibeDamage(4);

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
                vida.RecibeDamage(2);

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
        //if (!isAttacking)
        // moveCharacter(movement);
    }

    public void Damage(int damage)
    {
        

        int rand = random.Next(1, 6);

        if (rand == 1)
            SoundController.instance.PlaySound(Impact1, 0.6f);
        else if (rand == 2)
            SoundController.instance.PlaySound(Impact2, 0.6f);
        else if (rand == 3)
            SoundController.instance.PlaySound(Impact3, 0.6f);

        life -= damage;

        Alert = true;
        //particulas.Play();
        if (life <= 0)
        {
            StopAllCoroutines();

            Animator.Play("Death");

            agent.enabled = false;

            Animator.SetBool("Walk", false);
            Animator.SetBool("Attack", false);
            StartCoroutine(Morir());

            /*
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

            }*/
        }
    }

    /*
    public static IEnumerator PlaySound(AudioSource audioSrc, AudioClip sfx, float volume, float pitch = 1, float waitingTime = 0)
    {
        yield return new WaitForSeconds(waitingTime);

        audioSrc.clip = sfx;
        audioSrc.volume = volume;
        audioSrc.pitch = pitch;
        audioSrc.PlayOneShot(sfx);

        yield return "Audio Played";
    }
    */
    private IEnumerator Morir()
    {
        Animator.SetBool("Death", true);

        rb.simulated = false;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length);

    }

}

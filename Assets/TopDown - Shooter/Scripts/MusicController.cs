using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    private AudioSource audioSource;

    [SerializeField] private AudioClip audioIntro;
    [SerializeField] private AudioClip MusicHorde;

    [SerializeField] private AudioClip ZombieAmb1;
    [SerializeField] private AudioClip ZombieAmb2;

    [SerializeField] private AudioClip HordeTension1;
    [SerializeField] private AudioClip HordeTension2;

    private float timer = 0;
    private float timerFX = 0;
    public bool isHorde = false;

    public bool isHordeTension1 = true;

    public bool zombieAmb = false;
    public static bool isFirtsZombieAmb = true;

    private static bool intro = true;

    private void Start()
    {
        if (intro)
        {
            PlaySound(audioIntro, 0.2f);
            intro = false;
        }

    }

    private void Update()
    {
        if (!isHorde)
        {
            if (TimerFX() || isFirtsZombieAmb)
            {
                if (zombieAmb)
                {
                    PlayFXSound(ZombieAmb2, 0.3f);
                }
                else
                {
                    PlayFXSound(ZombieAmb1, 0.3f);
                    zombieAmb = true;
                }
                isFirtsZombieAmb = false;
                timerFX=0;
            }
        }

        if (isHorde)
        {
            ChangeMusic();
        }
    }

    private void ChangeMusic()
    {
        if (isHordeTension1)
        {
            PlayFXSound(HordeTension1, 0.3f);
            PlayFXSound(HordeTension2, 0.4f);
            isHordeTension1 = false;
        }

        if (Timer())
        {
            PlaySound(MusicHorde, 0.3f);
            isHorde = false;
        }
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audio, float volumen = 0.5f)
    {
        audioSource.volume = volumen;
        audioSource.loop = true;
        audioSource.PlayOneShot(audio);
    }

    public void PlayFXSound(AudioClip audio, float volumen = 0.5f)
    {
        audioSource.volume = volumen;
        audioSource.PlayOneShot(audio);
    }

    private bool Timer()
    {
        if (timer < 20)
        {
            timer += Time.deltaTime;

            return false;
        }
        else
        {
            return true;
        }
    }

    private bool TimerFX()
    {
        if (timerFX < 180)
        {
            timerFX += Time.deltaTime;

            return false;
        }
        else
        {
            return true;
        }
    }


}

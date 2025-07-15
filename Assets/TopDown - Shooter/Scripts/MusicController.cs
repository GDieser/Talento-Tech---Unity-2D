using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider miSlider;

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

        float vol = PlayerPrefs.GetFloat("fxVolume", 1f);
        miSlider.value = vol;

    }

    public void CambiarVolumen()
    {
        audioSource.volume = miSlider.value;
        PlayerPrefs.SetFloat("fxVolume", miSlider.value);
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
            PlayFXSound(HordeTension1, 0.4f);
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
        audioSource.volume = PlayerPrefs.GetFloat("fxVolume", 1f);
    }

    
    public void PlaySound(AudioClip audio, float volumen = 0.5f)
    {
        float volumenFinal = volumen * miSlider.value;
        audioSource.volume = volumenFinal;
        audioSource.loop = true;
        audioSource.PlayOneShot(audio);
    }

    /*
    public void PlaySound(AudioClip audio, float volumen = 0.5f, bool loop = false)
    {
        //audioSource.Stop(); // Detiene cualquier sonido anterior

        audioSource.clip = audio;
        audioSource.volume = volumen;
        audioSource.loop = loop;
        audioSource.Play();

    }*/

    public void PlaySirena(AudioClip audio, float volumen = 0.5f, bool loop = false)
    {
        audioSource.clip = audio;
        float volumenFinal = volumen * miSlider.value;
        audioSource.volume = volumenFinal;
        audioSource.loop = loop;
        audioSource.Play();

    }

    public void PlayMotor(AudioClip audio, float volumen = 0.5f, bool loop = false)
    {
        audioSource.clip = audio;
        float volumenFinal = volumen * miSlider.value;
        audioSource.volume = volumenFinal;
        audioSource.loop = loop;
        audioSource.Play();
    }


    public void PlayFXSound(AudioClip audio, float volumen = 0.5f)
    {
        float volumenFinal = volumen * miSlider.value;
        audioSource.volume = volumenFinal;
        audioSource.PlayOneShot(audio);
    }

    private bool Timer()
    {
        if (timer < 10)
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

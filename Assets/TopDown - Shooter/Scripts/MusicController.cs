using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Volver a enlazar si el slider de volumen es parte de la escena
        if (miSlider == null)
            miSlider = GameObject.Find("VolumenSlider")?.GetComponent<Slider>();

        // También podés restaurar referencias a audioClips o sources si fuera necesario
    }

    private void Update()
    {
        if (!isHorde)
        {
            if (TimerFX() || isFirtsZombieAmb)
            {
                if (!GameStateHorde.esHorda)
                {
                    if (zombieAmb)
                    {
                        if (ZombieAmb2 != null)
                            PlayFXSound(ZombieAmb2, 0.3f);
                    }
                    else
                    {
                        if (ZombieAmb1 != null)
                            PlayFXSound(ZombieAmb1, 0.3f);
                        zombieAmb = true;
                    }
                    isFirtsZombieAmb = false;
                    timerFX = 0;
                }
            }
        }

        if (isHorde)
        {
            ChangeMusic();
        }
    }

    public void DetenerMusica()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = null;
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
            GameStateHorde.esHorda = true;
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
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource no asignado");
            return;
        }

        if (audio == null)
        {
            Debug.LogWarning("AudioClip de sirena no asignado");
            return;
        }

        float volumenFinal = volumen * (miSlider != null ? miSlider.value : 1f);
        audioSource.clip = audio;
        audioSource.volume = volumenFinal;
        audioSource.loop = loop;
        audioSource.Play();
    }


    public void PlayMotor(AudioClip audio, float volumen = 0.5f, bool loop = false)
    {
        if (audioSource == null || audio == null)
        {
            Debug.LogWarning("Audio o AudioSource faltante en FX");
            return;
        }
        audioSource.clip = audio;
        float volumenFinal = volumen * miSlider.value;
        audioSource.volume = volumenFinal;
        audioSource.loop = loop;
        audioSource.Play();
    }


    public void PlayFXSound(AudioClip audio, float volumen = 0.5f)
    {
        if (audioSource == null || audio == null)
        {
            Debug.LogWarning("Audio o AudioSource faltante en FX");
            return;
        }
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

    public static class GameStateHorde
    {
        public static bool esHorda = false;

    }


}

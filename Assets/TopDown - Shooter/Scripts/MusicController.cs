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
    //[SerializeField] private Slider miSlider;

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

    private bool sonidoInicialReproducido = false;
    private static bool sonidoInicialReproducido2 = false;

    private static bool intro = true;
    private bool introReproducida = false;

    private void Start()
    {
        float vol = PlayerPrefs.GetFloat("fxVolume", 1f);

        /*if (miSlider == null)
        {
            Slider sliderEncontrado = GameObject.Find("MusicSlider")?.GetComponent<Slider>();
            if (sliderEncontrado != null)
            {
                miSlider = sliderEncontrado;
                miSlider.onValueChanged.AddListener(delegate { CambiarVolumen(); });
            }
        }

        if (miSlider != null)
            miSlider.value = vol;
        */
       

        if (intro)
        {
            
            PlaySound(audioIntro, 0.2f);
            intro = false;
        }
        else if (GameManager.instance.IsLevel2 && !sonidoInicialReproducido2)
        {
            
            PlaySound(audioIntro, 0.2f);
            sonidoInicialReproducido2 = true;
        }

    }

    /*
    public void CambiarVolumen()
    {
        audioSource.volume = miSlider.value;
        PlayerPrefs.SetFloat("fxVolume", miSlider.value);
    }
    */

    private void Update()
    {

        if (!isHorde)
        {
            if ((TimerFX() || isFirtsZombieAmb || (GameManager.instance.IsLevel2 && !sonidoInicialReproducido)))
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

                    if (GameManager.instance.IsLevel2)
                        sonidoInicialReproducido = true;
                }
            }
        }

        if (isHorde)
        {
            //Debug.Log("Horda");
            ChangeMusic();
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
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("fxVolume", 1f);
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
           // Debug.Log("entro");
            PlaySound(MusicHorde, 0.3f);
            isHorde = false;
            GameStateHorde.esHorda = true;
        }
    }




    public void PlaySound(AudioClip audio, float volumen = 0.5f)
    {
        //float volumenFinal = volumen; //* miSlider.value;
        audioSource.volume = volumen;
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
            return;
        }

        if (audio == null)
        {
            return;
        }

        float volumenFinal = volumen;// * (miSlider != null ? miSlider.value : 1f);
        audioSource.clip = audio;
        audioSource.volume = volumenFinal;
        audioSource.loop = loop;
        audioSource.Play();
    }


    public void PlayMotor(AudioClip audio, float volumen = 0.5f, bool loop = false)
    {
        if (audioSource == null || audio == null)
        {
            return;
        }
        audioSource.clip = audio;
        float volumenFinal = volumen;// * miSlider.value;
        audioSource.volume = volumenFinal;
        audioSource.loop = loop;
        audioSource.Play();
    }


    public void PlayFXSound(AudioClip audio, float volumen = 0.5f)
    {
        if (audioSource == null || audio == null)
        {
            Debug.LogWarning("AudioSource o AudioClip es null");
            return;
        }

        /*
        if (miSlider == null)
        {
            Debug.LogWarning("miSlider es null");
            return;
        }
        */
        float volumenFinal = volumen;// * miSlider.value;
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


        public static bool Level2 = false;

        public static void Reset()
        {
            esHorda = false;
            Level2 = false;
        }

    }

    public static void ResetInstance()
    {
        intro = true;
        isFirtsZombieAmb = true;

        sonidoInicialReproducido2 = false;

        if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = null;
        }
    }


}

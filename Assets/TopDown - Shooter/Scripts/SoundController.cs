using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;

    [SerializeField] private AudioSource audioSource;
    //[SerializeField] public Slider miSlider;

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


        audioSource.volume = PlayerPrefs.GetFloat("fxVolume", 1f);
    }

    void Start()
    {
        float vol = PlayerPrefs.GetFloat("fxVolume", 1f);
        //miSlider.value = vol;
        /*
        if (miSlider == null)
        {
            Slider sliderEncontrado = GameObject.Find("SoundSlider")?.GetComponent<Slider>();
            if (sliderEncontrado != null)
            {
                miSlider = sliderEncontrado;
                miSlider.onValueChanged.AddListener(delegate { CambiarVolumen(); });
            }
        }*/
        /*
        if (miSlider != null)
        {
            miSlider.value = vol;
        }*/
    }

    public void DetenerFX()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    /*
    public void CambiarVolumen()
    {
        audioSource.volume = miSlider.value;
        PlayerPrefs.SetFloat("fxVolume", miSlider.value);
    }
    */

    public void PlaySound(AudioClip audio, float volumenRelativo = 1f)
    {
        float volumenFinal = volumenRelativo * audioSource.volume;
        audioSource.PlayOneShot(audio, volumenFinal);
    }


    public void PlaySound2(AudioClip audio, float volumenRelativo = 1f)
    {
        PlaySound(audio, volumenRelativo);
    }

    public static void ResetInstance()
    {
        if (instance != null)
        {
            //instance.miSlider = null;
            instance.audioSource = null;

            Destroy(instance.gameObject);
            instance = null;
        }
    }

}

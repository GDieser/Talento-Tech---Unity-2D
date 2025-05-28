using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;

    private AudioSource audioSource;

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

    public void PlaySound(AudioClip audio, float volumen)
    {
        audioSource.volume = volumen;
        audioSource.PlayOneShot(audio);
    }

    public void PlaySoundBackground(AudioClip audio, float volumen)
    {
        audioSource.volume = volumen;
        audioSource.PlayOneShot(audio);
    }

}

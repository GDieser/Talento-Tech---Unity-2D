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

    private float timer = 0;
    public bool isHorde = false;

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
        if(isHorde)
        {
            ChangeMusic();
        }
    }

    private void ChangeMusic()
    {
        if(Timer())
        {
            PlaySound(MusicHorde, 0.4f);
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


}

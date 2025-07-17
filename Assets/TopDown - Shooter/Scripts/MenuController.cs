using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject principal;
    [SerializeField] private GameObject nuevaPartida;
    [SerializeField] private GameObject configuracion;
    [SerializeField] private GameObject creditos;

    private AudioSource aSource;
    [SerializeField] private AudioClip audio;
    [SerializeField] private AudioClip audio2;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void NuevaPartida(bool abrir)
    {
        if(abrir)
        {
            aSource.PlayOneShot(audio);
            principal.SetActive(false);
            nuevaPartida.SetActive(true);
        }
        else
        {
            aSource.PlayOneShot(audio);
            principal.SetActive(true);
            nuevaPartida.SetActive(false);
        }
    }

    public void Configuracion(bool abrir)
    {
        if (abrir)
        {
            aSource.PlayOneShot(audio);
            principal.SetActive(false);
            configuracion.SetActive(true);
        }
        else
        {
            aSource.PlayOneShot(audio);
            principal.SetActive(true);
            configuracion.SetActive(false);
        }
    }

    public void Creditos(bool abrir)
    {
        if (abrir)
        {
            aSource.PlayOneShot(audio);
            principal.SetActive(false);
            creditos.SetActive(true);
        }
        else
        {
            aSource.PlayOneShot(audio);
            principal.SetActive(true);
            creditos.SetActive(false);
        }
    }

    public void AbrirLevel1()
    {
        //MusicController.instance.DetenerMusica();
        //SoundController.instance.DetenerFX();
        aSource.PlayOneShot(audio2);
        SceneManager.LoadScene("IntroCiudad");
    }

    public void AbrirLevel2()
    {
        //MusicController.instance.DetenerMusica();
        //SoundController.instance.DetenerFX();
        aSource.PlayOneShot(audio2);
        SceneManager.LoadScene("IntroHospital");
    }


    public void Salir()
    {
        Application.Quit();
    }
}

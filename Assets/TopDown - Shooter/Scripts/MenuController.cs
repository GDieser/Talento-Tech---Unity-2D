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


    public void NuevaPartida(bool abrir)
    {
        if(abrir)
        {
            principal.SetActive(false);
            nuevaPartida.SetActive(true);
        }
        else
        {
            principal.SetActive(true);
            nuevaPartida.SetActive(false);
        }
    }

    public void Configuracion(bool abrir)
    {
        if (abrir)
        {
            principal.SetActive(false);
            configuracion.SetActive(true);
        }
        else
        {
            principal.SetActive(true);
            configuracion.SetActive(false);
        }
    }

    public void Creditos(bool abrir)
    {
        if (abrir)
        {
            principal.SetActive(false);
            creditos.SetActive(true);
        }
        else
        {
            principal.SetActive(true);
            creditos.SetActive(false);
        }
    }

    public void AbrirLevel1()
    {
        SceneManager.LoadScene("IntroCiudad");
    }

    public void AbrirLevel2()
    {
        SceneManager.LoadScene("IntroHospital");
    }


    public void Salir()
    {
        Application.Quit();
    }
}

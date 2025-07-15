using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TransitionScene1 : MonoBehaviour
{
    [SerializeField] private GameObject animation;
    [SerializeField] private GameObject staticCanva;
    [SerializeField] private float duracionAnimacion = 2f;
    [SerializeField] private string nombreSiguienteEscena;
    [SerializeField] private GameObject textoPresionaE;

    private AsyncOperation cargaEscena;

    private bool puedeCambiarEscena = false;

    void Start()
    {
        animation.SetActive(true);
        staticCanva.SetActive(false);
        textoPresionaE.SetActive(false);

        StartCoroutine(MostrarIntroYPreCargar());
    }

    void Update()
    {
        if (puedeCambiarEscena && Input.GetKeyDown(KeyCode.E))
        {
            cargaEscena.allowSceneActivation = true;
        }
    }

    private IEnumerator MostrarIntroYPreCargar()
    {
        // Empieza a cargar la escena en segundo plano pero no la activa aún
        cargaEscena = SceneManager.LoadSceneAsync(nombreSiguienteEscena);
        cargaEscena.allowSceneActivation = false;

        yield return new WaitForSeconds(duracionAnimacion);

        animation.SetActive(false);
        staticCanva.SetActive(true);

        // Esperar a que la carga esté casi completa
        while (cargaEscena.progress < 0.9f)
        {
            yield return null; // espera un frame
        }

        textoPresionaE.SetActive(true);
        puedeCambiarEscena = true;
    }


}

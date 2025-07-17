using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TransitionScene1 : MonoBehaviour
{
    [SerializeField] private GameObject animation;
    [SerializeField] private GameObject staticCanva;
    [SerializeField] private float duracionAnimacion = 2f;
    [SerializeField] private string nombreSiguienteEscena;
    [SerializeField] private GameObject textoPresionaE;
    [SerializeField] private Slider sliderCarga;

    private bool puedeCambiarEscena = false;
    private AsyncOperation operacionCarga;

    void Start()
    {
        animation.SetActive(true);
        staticCanva.SetActive(false);
        textoPresionaE.SetActive(false);
        sliderCarga.value = 0f;

        StartCoroutine(MostrarIntro());
    }

    void Update()
    {
        if (puedeCambiarEscena && Input.GetKeyDown(KeyCode.E))
        {
            operacionCarga.allowSceneActivation = true;
        }
    }

    private IEnumerator MostrarIntro()
    {
        yield return new WaitForSeconds(duracionAnimacion);

        animation.SetActive(false);
        staticCanva.SetActive(true);

        // Inicia la carga asincrónica sin activar aún
        StartCoroutine(CargarEscena());
    }

    private IEnumerator CargarEscena()
    {
        operacionCarga = SceneManager.LoadSceneAsync(nombreSiguienteEscena);
        operacionCarga.allowSceneActivation = false;

        while (!operacionCarga.isDone)
        {
            // Unity frena el progreso en 0.9 hasta que se permita la activación
            float progreso = Mathf.Clamp01(operacionCarga.progress / 0.9f);
            sliderCarga.value = progreso;

            if (progreso >= 1f)
            {
                textoPresionaE.SetActive(true);
                puedeCambiarEscena = true;
            }

            yield return null;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class GeneratorController : MonoBehaviour
{
    [SerializeField] private AudioClip generatorClip1;
    [SerializeField] private AudioClip generatorClip2;

    [SerializeField] private AudioClip sirenaClip1;
    [SerializeField] private AudioClip sirenaClip2;
    [SerializeField] private AudioClip sirenaClip3;

    [SerializeField] private RadioController controller;


    [SerializeField] private TextMeshProUGUI textMesh;

    [SerializeField] private GameObject Faro1;
    [SerializeField] private GameObject Faro2;

    public bool IsActive = false;
    private bool isInRange = false;
    private bool isHorde = false;

    private bool reinicio = false;

    private void Update()
    {
        if (GameStateGenerator.HordaActiva && !reinicio)
        {
            StartCoroutine(EncenderSirenas());

            Faro1.SetActive(true);
            Faro2.SetActive(true);

            reinicio = true;
        }
        else
        {

            if (isInRange && Input.GetKeyDown(KeyCode.E) && !IsActive)
            {
                ActivarGenerador();
            }

            if (!isHorde)
            {
                ActivarHorda(controller.IsActive);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameStateGenerator.HordaActiva)
        {
            if (collision.CompareTag("Player") && !IsActive)
            {
                isInRange = true;

                if (!IsActive)
                    textMesh.text = "Presiona E para activar el generador";

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            textMesh.text = "";
        }
    }

    private void ActivarGenerador()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            textMesh.text = "";
            IsActive = true;

            StartCoroutine(EncenderGenerador());

        }

    }

    private void ActivarHorda(bool radio)
    {
        if (radio)
        {
            isHorde = true;
            StartCoroutine(EncenderSirenas());
        }


    }

    private IEnumerator EncenderSirenas()
    {
        SoundController.instance.PlaySound(sirenaClip1, 0.8f);

        yield return new WaitForSeconds(sirenaClip1.length);


        MusicController.instance.PlaySirena(sirenaClip2, 0.5f, true);
        //SoundController.instance.PlaySound(sirenaClip3, 0.8f);

        GameStateGenerator.HordaActiva = true;
    }


    private IEnumerator EncenderGenerador()
    {

        SoundController.instance.PlaySound(generatorClip1, 0.8f);

        yield return new WaitForSeconds(generatorClip1.length);


        MusicController.instance.PlaySound(generatorClip2, 0.6f);
        Faro1.SetActive(true);
        Faro2.SetActive(true);

        

    }

    public static class GameStateGenerator
    {
        public static bool HordaActiva = false;
    }


}

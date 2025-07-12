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

    public bool IsActive = false;
    private bool isInRange = false;
    private bool isHorde = false;

    private void Update()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !IsActive)
        {
            isInRange = true;

            if (!IsActive)
                textMesh.text = "Presiona E para activar el generador";

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
            IsActive = true;

            StartCoroutine(EncenderGenerador());

        }

        /*
        SoundController.instance.PlaySound(sirenaClip1, 0.8f);
        SoundController.instance.PlaySound(sirenaClip2, 0.8f);
        SoundController.instance.PlaySound(sirenaClip3, 0.8f);
        */
    }

    private void ActivarHorda(bool radio)
    {
        if (radio)
        {
            isHorde = true;
            //SoundController.instance.PlaySound(sirenaClip1, 0.8f);
            MusicController.instance.PlaySound(sirenaClip2);
            //SoundController.instance.PlaySound(sirenaClip3, 0.8f);
        }


    }

    private IEnumerator EncenderGenerador()
    {
        // 1. Sonido inicial (una vez)
        SoundController.instance.PlaySound(generatorClip1, 0.8f);

        yield return new WaitForSeconds(generatorClip1.length); // o yield return new WaitForSeconds(3f);

        // 3. Reproducir en loop (desde otro controlador si querés)
        MusicController.instance.PlaySound(generatorClip2);
    }


}

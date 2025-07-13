using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static MenuPause;

public class RadioController : MonoBehaviour
{
    [SerializeField] private GeneratorController controller;
    [SerializeField] private TextMeshProUGUI textMesh;

    [SerializeField] private AudioClip RadioClip1;

    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField]
    private string mensajeRadio = "¿Hola? ¿Me escuchas? ¿Pudiste llegar al hospital?\n" +
               "Hay poco tiempo. Necesitás encontrar 3 medikits grandes y una caja de herramientas.\n" +
               "El auto está en el estacionamiento trasero. ¡Mucha suerte!";
    [SerializeField] private float duracionDialogo = 20f;

    [SerializeField] GameObject radioCom;

    [SerializeField] private MenuPause menu;
    [SerializeField] private MusicController music;
    [SerializeField] private HordeController horde;

    public bool IsActive = false;
    private bool isInRange = false;

    private bool play = false;
    private bool activo = false;
    private float timer = 0;

    private bool reinicio = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (GameStateRadio.HordaActiva && !reinicio)
        {
            textMesh.text = "";
            music.isHorde = true;
            horde.enabled = true;
            activo = true;

            reinicio = true;
        }
        else
        {

            if (isInRange && Input.GetKeyDown(KeyCode.E) && controller.IsActive && !IsActive && !play)
            {

                textMesh.text = "";
                StartCoroutine(AccionRadio());

            }

            if (play && Timer() && !activo)
            {
                music.isHorde = true;
                horde.enabled = true;
                activo = true;
            }
        }
    }

    private bool Timer()
    {
        if (timer < 35)
        {
            timer += Time.deltaTime;

            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!GameStateRadio.HordaActiva)
        {
            if (collision.CompareTag("Player"))
            {
                isInRange = true;

                if (controller.IsActive && !IsActive)
                {
                    textMesh.text = "Presiona E para llamar a la base";

                }
                else if (IsActive)
                    textMesh.text = "";
                else
                    textMesh.text = "Sin energía, activa el generador";
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

    public void ActivarRadio()
    {
        if (Input.GetKeyDown(KeyCode.E) && controller.IsActive && !play)
        {

            StartCoroutine(AccionRadio());

        }
    }

    private IEnumerator TypeDialogue(string text, float duration)
    {
        dialogText.text = "";
        float delay = duration / text.Length;

        foreach (char c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator AccionRadio()
    {
        play = true;
        radioCom.SetActive(true);
        // 1. Sonido inicial (una vez)
        SoundController.instance.PlaySound(RadioClip1, 0.8f);

        // 2. Lanzar diálogo al mismo tiempo
        StartCoroutine(TypeDialogue(mensajeRadio, duracionDialogo));

        // 3. Esperar a que termine el sonido + un poco más
        yield return new WaitForSeconds(RadioClip1.length + 1f);

        // 4. Marcar como activado
        IsActive = true;
        radioCom.SetActive(false);

        GameState.SecondSpawnLevel1 = true;
        GameStateRadio.HordaActiva = true;

    }

    public static class GameStateRadio
    {
        public static bool HordaActiva = false;

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static MenuPause;
using static PlayerVida;

public class RadioController : MonoBehaviour
{
    [SerializeField] private GeneratorController controller;
    [SerializeField] private TextMeshProUGUI textMesh;

    [SerializeField] private AudioClip RadioClip1;

    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField]
    private string mensajeRadio = "... ... Ho... ¿Hola?... ¿Me escuchas?... ¿Pudiste llegar al hospital?\n" +
               "Hay poco tiempo. Necesitás encontrar 3 equipos medicos y una caja de herramientas para conectar la batería del auto.\n" +
               "El auto quedó en el estacionamiento trasero, tenes que salir por el deposito. Nos encontramos en el punto de extracción. Mucha suerte...";

    private string mensajeRadio2 = "... ... Ho... ¿Hola?... ¿Me escuchas?... ¿Pudiste conseguir todo?\n" +
               "Ahora necesitas esperar a que cargue la batería del auto, eso va a tardar y generar mucho ruido, vas a tener que resistir lo suficiente para que arranque.\n" +
               "Te vamos a estar esperando en el punto de extracción. Mucha suerte...";

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

    [SerializeField] private bool level2 = false;
    [SerializeField] private TextMeshProUGUI instrucciones;

    [SerializeField] private GameObject EndingCanva;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!level2 || GameStateRadio.bateriaConectada)
        {
            if (!GameStateStory.sirena2 || !GameStateStory.sirena1)
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
            else if (GameStateStory.sirena1 && GameStateStory.sirena2)
            {
                if (isInRange && Input.GetKeyDown(KeyCode.E) && controller.IsActive && !IsActive && !play)
                {

                    textMesh.text = "";
                    StartCoroutine(AccionRadioAlt());

                    instrucciones.text = "Entra en el Hospital";

                }
            }
        }
        else
        {

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
        Debug.Log(GameStateRadio.HordaActiva);

        if (!GameStateRadio.HordaActiva)
        {
            if (collision.CompareTag("Player"))
            {
                isInRange = true;

                if (!level2)
                {
                    if (controller.IsActive && !IsActive)
                    {
                        textMesh.text = "Presiona E para llamar a la base";

                    }
                    else if (IsActive)
                        textMesh.text = "";
                    else
                        textMesh.text = "Sin energía, activa el generador";
                }
                else
                {
                    if (GameStateRadio.bateriaConectada)
                    {
                        if (controller.IsActive && !IsActive)
                        {
                            textMesh.text = "Presiona E para llamar a la base";

                        }
                        else if (IsActive)
                            textMesh.text = "";

                    }
                    else if (!controller.IsActive && !IsActive)
                    {

                        textMesh.text = "Sin energía, activa el generador";
                    }
                    else
                    {
                        textMesh.text = "Conecta primero la batería del auto";
                    }
                }
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

        SoundController.instance.PlaySound(RadioClip1, 0.8f);

        if (!level2)
            StartCoroutine(TypeDialogue(mensajeRadio, duracionDialogo));
        else
            StartCoroutine(TypeDialogue(mensajeRadio2, duracionDialogo));


        yield return new WaitForSeconds(RadioClip1.length + 5f);

        IsActive = true;
        radioCom.SetActive(false);

        GameState.SecondSpawnLevel1 = true;
        GameStateRadio.HordaActiva = true;

    }

    private IEnumerator AccionRadioAlt()
    {
        play = true;
        radioCom.SetActive(true);

        SoundController.instance.PlaySound(RadioClip1, 0.8f);


        StartCoroutine(TypeDialogue(mensajeRadio, duracionDialogo));

        yield return new WaitForSeconds(RadioClip1.length + 5f);
        
        radioCom.SetActive(false);

        EndingCanva.SetActive(true);

    }

    public static class GameStateRadio
    {
        public static bool HordaActiva = false;
        public static bool bateriaConectada = false;

        public static void ResetAll()
        {
            HordaActiva = false;
            bateriaConectada = false;
        }

    }


}

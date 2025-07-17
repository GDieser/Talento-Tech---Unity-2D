using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class ChildStory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI instrucciones;
    [SerializeField] private TextMeshProUGUI interaccion;

    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private PlayerVida player;

    [SerializeField] private GameObject radioCom;

    [SerializeField] private AudioClip audio;

    private string dialogo = "¿Hola...? ¿Señor...? Tenga cuidado, hay muchos monstruos en la calle...  Ayer mi papá fue al hospital y aún no volvió... Espero que este bien, necesitamos medicamentos para mamá... Ayer sonó una alarma muy fuerte y después muchos gritos... Si vez a mi papá por favor ayudalo, usaba una remera roja, mi mamá lo necesita...";

    private bool IsRange = false;
    private bool hablando = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRange && !hablando && Input.GetKeyDown(KeyCode.E))
        {
            
            StartCoroutine(TypeDialogue(dialogo, 24));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Debug.Log(player.GetStory());

            IsRange = true;

            interaccion.text = "Presiona E para hablar";

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsRange = false;
            interaccion.text = "";

        }
    }

    private IEnumerator TypeDialogue(string text, float duration)
    {
        hablando = true;
        interaccion.text = "";

        radioCom.SetActive(true);
        dialogText.text = "";

        float delay = duration / text.Length;

        SoundController.instance.PlaySound(audio, 0.4f);

        foreach (char c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(1f);

        player.SetStory(1);

        radioCom.SetActive(false);
        hablando = false;

        if (IsRange)
        {
            interaccion.text = "Presiona E para hablar";
        }
    }

}

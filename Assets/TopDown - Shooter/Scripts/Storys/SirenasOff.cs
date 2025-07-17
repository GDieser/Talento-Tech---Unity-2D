using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PlayerVida;

public class SirenasOff : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoInteraccion;
    [SerializeField] private PlayerVida player;

    [SerializeField] private bool sirena1;

    private bool apagada = false;

    private bool range = false;

    private void Update()
    {
        if (range && Input.GetKeyDown(KeyCode.E))
        {
            if(sirena1)
            {
                Debug.Log("Entro 1");
                player.SetStory(3);
                apagada = true;
            }
            else
            {
                Debug.Log("Entro 2");
                player.SetStory(4);
                apagada = true;
            }

            textoInteraccion.text = "";

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameStateStory.hablo && !apagada)
        {
            range = true;
            textoInteraccion.text = "Presiona E para desactivar Sirena";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            range = false;
            textoInteraccion.text = "";
        }
    }

}

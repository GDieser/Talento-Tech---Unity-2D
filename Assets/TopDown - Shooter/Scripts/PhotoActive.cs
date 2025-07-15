using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhotoActive : MonoBehaviour
{
    public TextMeshProUGUI textoInteraccion;
    public GameObject panelFoto;
    [SerializeField] private PlayerVida player;

    private bool jugadorEnZona = false;
    private bool fotoMostrada = false;

    void Update()
    {
        if (jugadorEnZona && !fotoMostrada && Input.GetKeyDown(KeyCode.E))
        {
            fotoMostrada = true;
            panelFoto.SetActive(true);

            Invoke("DesactivarFoto", 5f);

            player.SetStory(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !fotoMostrada)
        {
            jugadorEnZona = true;
            textoInteraccion.text = "Presiona E para ver foto";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = false;
            textoInteraccion.text = "";
        }
    }

    private void DesactivarFoto()
    {
        panelFoto.SetActive(false);
        gameObject.SetActive(false);
    }
}

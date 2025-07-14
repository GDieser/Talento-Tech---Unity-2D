using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GeneratorController;
using static RadioController;

public class VehiculoController : MonoBehaviour
{
    [SerializeField] private AudioClip capoOpen;
    [SerializeField] private AudioClip autoStart;

    [SerializeField] private TextMeshProUGUI textMesh;

    [SerializeField] private GeneratorController controller;
    [SerializeField] private TextMeshProUGUI instrucciones;

    public bool IsActive = false;
    private bool isInRange = false;
    private bool isHorde = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !IsActive)
        {
            ActivarBateria();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameStateAuto.HordaActiva)
        {
            if (collision.CompareTag("Player") && !IsActive)
            {
                isInRange = true;

                if (controller.IsActive && !IsActive)
                {
                    textMesh.text = "Presiona E para conectar la batería";

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

    private void ActivarBateria()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            textMesh.text = "";
            IsActive = true;

            StartCoroutine(CargarBateria());

        }
    }

    private IEnumerator CargarBateria()
    {

        SoundController.instance.PlaySound(capoOpen, 0.8f);

        yield return new WaitForSeconds(capoOpen.length);

        GameStateAuto.HordaActiva = true;
        GameStateRadio.bateriaConectada = true;

        instrucciones.text = "Ve al radio y contacta con tu equipo.";
    }

    public static class GameStateAuto
    {
        public static bool HordaActiva = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioController : MonoBehaviour
{
    [SerializeField] private GeneratorController controller;
    [SerializeField] private TextMeshProUGUI textMesh;

    [SerializeField] private AudioClip RadioClip1;

    public bool IsActive = false;
    private bool isInRange = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && controller.IsActive && !IsActive)
        {
            ActivarRadio();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;

            if (controller.IsActive && !IsActive)
                textMesh.text = "Presiona E para llamar a la base";
            else if (IsActive)
                textMesh.text = "";
            else
                textMesh.text = "Sin energía, activa el generador";
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
        if (Input.GetKeyDown(KeyCode.E) && controller.IsActive)
        {
            StartCoroutine(AccionRadio());

        }
    }

    private IEnumerator AccionRadio()
    {
        // 1. Sonido inicial (una vez)
        SoundController.instance.PlaySound(RadioClip1, 0.8f);

        yield return new WaitForSeconds(RadioClip1.length + 1f); // o yield return new WaitForSeconds(3f);

        IsActive = true;
    }


}

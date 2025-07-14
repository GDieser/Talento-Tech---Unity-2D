using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending2Active : MonoBehaviour
{
    [SerializeField] private GameObject ending;
    [SerializeField] private AudioClip startCar;
    [SerializeField] private PlayerMov player;

    private bool visto = false;

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ending.SetActive(true);

            if (!visto)
            {
                SoundController.instance.PlaySound(startCar, 0.8f);
                visto = true;
            }

            if (player != null)
                player.enabled = false;

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            this.gameObject.SetActive(false);

            // (Opcional) Transición de escena
            //StartCoroutine(CargarEscenaFinal());
        }
    }

    /*
    private IEnumerator CargarEscenaFinal()
    {
        yield return new WaitForSeconds(4f); // Esperás 4 segundos por ejemplo
        SceneManager.LoadScene("FinalScene"); // Cambiá por el nombre de tu escena final
    }
    */

}

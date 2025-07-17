using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject Hud;
    private void Start()
    {
        Hud.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VolverMenuPrinciapl()
    {
        Destroy(MusicController.instance.gameObject);
        Destroy(SoundController.instance.gameObject);

        if (GameManager.instance != null)
            GameManager.instance.ResetEstado();

        SceneManager.LoadScene("Menu");
    }

}

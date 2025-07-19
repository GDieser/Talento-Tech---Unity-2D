using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GeneratorController;
using static MenuPause;
using static MusicController;
using static PlayerMission;
using static PlayerVida;
using static RadioController;
using static VehiculoController;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject Hud;
    private void Start()
    {
        Hud.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VolverMenuPrinciapl()
    {
        ResetearTodo();
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }


    public static void ResetearTodo()
    {
        if (MusicController.instance != null)
            MusicController.ResetInstance();

        if (SoundController.instance != null)
            SoundController.ResetInstance();

        if (GameManager.instance != null)
        {
            GameManager.instance.ResetEstado();
            GameObject.Destroy(GameManager.instance.gameObject);
            GameManager.instance = null;
        }

        GameState.ResetAll();
        GameStateStory.Reset();
        GameStateItems.ResetAll();
        GameStateGenerator.ResetAll();
        GameStateHorde.Reset();
        GameStateRadio.ResetAll();
        GameStateAuto.ResetAll();

        Intro.ResetIntroState();
        HordeCanvaScript.ResetIVistoState();
    }

}

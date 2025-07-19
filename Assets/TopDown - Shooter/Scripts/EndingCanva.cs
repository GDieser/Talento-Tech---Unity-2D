using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerVida;
using UnityEngine.SceneManagement;
using static GeneratorController;
using static MenuPause;
using static MusicController;
using static PlayerMission;
using static RadioController;
using static VehiculoController;

public class EndingCanva : MonoBehaviour
{
    [SerializeField] private GameObject Ending;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject instruccion;
    [SerializeField] private GameObject balas;

    [SerializeField] private PlayerShotRevolver revolver;
    [SerializeField] private PlayerShotRifle rifle;

    [SerializeField] private PlayerVida packs;


    [SerializeField] private bool IsLevel2 = false;


    private bool animation = false;

    private float timer = 0;

    private void Start()
    {
        Ending.SetActive(true);
        hud.SetActive(false);
        instruccion.SetActive(false);
        balas.SetActive(false);
        animation = true;

        //Debug.Log(rifle.totalBullets);
        //Debug.Log(revolver.totalBullets);
        //Debug.Log(packs.totalPacks);


        //Debug.Log("Guardado en GameManager: " + GameManager.instance.totalRevolverBullets);
        
        GameManager.instance.totalRifleBullets = rifle.totalBullets;
        GameManager.instance.totalPacks = packs.totalPacks;
        GameManager.instance.totalRevolverBullets = revolver.totalBullets;

        GameManager.instance.hablo = GameStateStory.hablo;
        GameManager.instance.sirena1 = false;
        GameManager.instance.sirena2 = false;

        
        
    }


    // Update is called once per frame
    void Update()
    {
        

        if (!GameManager.instance.IsLevel2 && !IsLevel2)
        {
            if (animation)
            {
                if (Timer())
                {
                    player.SetActive(false);

                    GameManager.instance.IsLevel2 = true;

                    Destroy(MusicController.instance.gameObject);
                    Destroy(SoundController.instance.gameObject);

                    GameStateStory.sirena1 = false;
                    GameStateStory.sirena2 = false;

                    SceneManager.LoadScene("IntroHospital");
                }
            }
        }
        else
        {
            if (animation)
            {
                if (Timer(14))
                {
                    if(player)
                        player.SetActive(false);

                    ResetearTodo();
                    Time.timeScale = 1f;
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    SceneManager.LoadScene("Menu");
                }
            }
            
        }

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

    private bool Timer(int time = 5)
    {
        if (timer < time)
        {
            timer += Time.deltaTime;

            return false;
        }
        else
        {
            return true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GeneratorController;
using static MusicController;
using static PlayerMission;
using static PlayerVida;
using static RadioController;
using static VehiculoController;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject menuConfig;
    [SerializeField] private GameObject playerPausa;
    private bool pauseGame = false;

    //[SerializeField] private Texture2D cursorMenu;
    //[SerializeField] private Vector2 cursorHotspot;

    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Vector2 cursorHotspotDefault;

    [SerializeField] private GameObject Hud;

    private float startX = 0;
    private float startY = 0;


    [SerializeField] private bool level1 = false;
    public bool SecondSpawnLevel1 = false;

    [SerializeField] private bool level2 = false;
    protected int spawmOrder = 0;

    public bool SecondSpawnLevel2 = false;

    [SerializeField] private GameObject missionHud;

    private void Awake()
    {
        

        if (level1 && !GameState.SecondSpawnLevel1)
            GameState.startPosition = new Vector2(-9f, -0.6f);
        else if (level1 && GameState.SecondSpawnLevel1)
            GameState.startPosition = new Vector2(22.5f, -0.3f);
        else if (level2 && !GameState.SecondSpawnLevel2 && !GameState.SecondSpawnAltLevel2)
            GameState.startPosition = new Vector2(-8, -0.5f);
        else if (level2 && GameState.SecondSpawnLevel2 && !GameState.SecondSpawnAltLevel2)
        {
            GameState.startPosition = new Vector2(-9, -38f);
            missionHud.SetActive(false);
        }
        else if (level2 && GameState.SecondSpawnAltLevel2)
            GameState.startPosition = new Vector2(64, 10f);
    }

    private void Start()
    {
        
        //GameState.startPosition = new Vector2(startX, startY);
        Time.timeScale = 1f;

    }

    void Update()
    {
        if (SecondSpawnLevel1)
            GameState.startPosition = new Vector2(22.5f, -0.3f);


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    protected void SetSpawnOrder(int order)
    {
        spawmOrder = order;
    }

    public void PauseGame()
    {
        //Cursor.SetCursor(cursorMenu, cursorHotspot, CursorMode.Auto);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Hud.SetActive(false);

        menuPausa.SetActive(true);
        playerPausa.SetActive(false);
        Time.timeScale = 0f;
        pauseGame = true;


    }
    public void ContinueGame()
    {
        Cursor.SetCursor(cursorDefault, cursorHotspotDefault, CursorMode.Auto);
        Hud.SetActive(true);
        menuPausa.SetActive(false);
        playerPausa.SetActive(true);
        Time.timeScale = 1f;
        pauseGame = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetSpawn(int level = 1)
    {
        if (level == 1)
            SecondSpawnLevel1 = true;
        if (level == 2)
        {
            GameState.SecondSpawnLevel2 = true;
            GameState.SecondSpawnAltLevel2 = false;
            missionHud.SetActive(false);
        }
        if (level == 3)
        {
            GameState.SecondSpawnAltLevel2 = true;
            GameState.SecondSpawnLevel2 = false;
        }
    }

    public void AbrirConfiguraciones(bool abrir)
    {
        if (abrir)
        {
            menuPausa.SetActive(false);
            menuConfig.SetActive(true);
        }
        else
        {
            menuConfig.SetActive(false);
            menuPausa.SetActive(true);
        }
    }

    public void VolverMenuPrinciapl()
    {
        ResetearTodo();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public static class GameState
    {
        public static Vector2 startPosition = new Vector2(19, -0.6f);

        public static bool SecondSpawnLevel1 = false;
        public static bool SecondSpawnLevel2 = false;
        public static bool SecondSpawnAltLevel2 = false;

        public static void ResetAll()
        {
            startPosition = new Vector2(19, -0.6f);
            SecondSpawnLevel1 = false;
            SecondSpawnLevel2 = false;
            SecondSpawnAltLevel2 = false;

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
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject playerPausa;
    private bool pauseGame = false;

    [SerializeField] private Texture2D cursorMenu;
    [SerializeField] private Vector2 cursorHotspot;

    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Vector2 cursorHotspotDefault;

    [SerializeField] private GameObject Hud;

    private float startX = 0;
    private float startY = 0;


    [SerializeField] private bool level1 = false;
    private bool SecondSpawnLevel1 = false;
    
    [SerializeField] private bool level2 = false;
    protected int spawmOrder = 0;

    private bool SecondSpawnLevel2 = false;


    private void Awake()
    {
        if (level1)
            GameState.startPosition = new Vector2(19, -0.6f);
        else if (level2)
            GameState.startPosition = new Vector2(-8, -0.5f);
    }

    private void Start()
    {
        //GameState.startPosition = new Vector2(startX, startY);
        Time.timeScale = 1f;

    }

    void Update()
    {
        if(SecondSpawnLevel1)
            GameState.startPosition = new Vector2(22, -0.3f);


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseGame)
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
        Cursor.SetCursor(cursorMenu, cursorHotspot, CursorMode.Auto);
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

    public void SetSpawn()
    {
        SecondSpawnLevel1 = true;
    }

    public static class GameState
    {
        public static Vector2 startPosition = new Vector2(19, -0.6f);
    }
}

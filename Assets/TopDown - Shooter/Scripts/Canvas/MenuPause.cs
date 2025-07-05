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

    private float startX = 0;
    private float startY = 0;

    private bool SecondSpawn = false;

    private void Start()
    {
        //GameState.startPosition = new Vector2(startX, startY);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if(SecondSpawn)
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

    public void PauseGame()
    {
        Cursor.SetCursor(cursorMenu, cursorHotspot, CursorMode.Auto);

        menuPausa.SetActive(true);
        playerPausa.SetActive(false);
        Time.timeScale = 0f;
        pauseGame = true;


    }
    public void ContinueGame()
    {
        Cursor.SetCursor(cursorDefault, cursorHotspotDefault, CursorMode.Auto);

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
        SecondSpawn = true;
    }

    public static class GameState
    {
        public static Vector2 startPosition = new Vector2(-9, -0.6f);
    }
}

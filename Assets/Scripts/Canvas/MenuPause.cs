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


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
}

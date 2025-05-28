using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject playerPausa;
    [SerializeField] private Animator animIntroText;

    private static bool introYaMostrada = false;

    private float timer = 0;

    void Start()
    {
        if (introYaMostrada)
        {
            this.gameObject.SetActive(false);
            menuPausa.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!Timer())
        {
            Pause();
        }
        else
        {
            introYaMostrada = true;
            menuPausa.SetActive(true);
            //playerPausa.SetActive(true);
            //Time.timeScale = 1f;
            this.gameObject.SetActive(false);
        }
    }

    private void Pause()
    {
        menuPausa.SetActive(false);
        //playerPausa.SetActive(false);
        //Time.timeScale = 0f;
    }

    private bool Timer()
    {
        if (timer < 20)
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

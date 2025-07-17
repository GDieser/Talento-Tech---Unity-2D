using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
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
            this.gameObject.SetActive(false);
        }
    }

    private void Pause()
    {
        menuPausa.SetActive(false);
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

    public static void ResetIntroState()
    {
        introYaMostrada = false;
    }

}

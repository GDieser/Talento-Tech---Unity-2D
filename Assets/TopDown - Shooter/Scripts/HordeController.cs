using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeController : MonoBehaviour
{
    [SerializeField] private HordeTimer TextTimer;
    [SerializeField] private GameObject TextHorde;

    private bool fase1 = false;
    private bool fase2 = false;
    private bool fase3 = false;

    private bool finFase = false;

    [SerializeField] GameObject Ending;

    private float timer = 0;
    private float totalIntoTime = 0;
    private int TextSec = 120;

    private bool viewCont = false;
    private float contador = 0;

    [SerializeField] private GameObject SpawnZombie;

    private void Start()
    {
        //TextTimer.enabled = true;
        TextHorde.SetActive(true);
    }

    private void Update()
    {
        TextTimer.setTimer(TextSec);


        if (!fase1)
            StartFase1();
        else if (!fase2)
            StartFase2();
        else if (!fase3)
            StartFase3();

        if(finFase)
        {
            Ending.SetActive(true);
            TextHorde.SetActive(false);
        }

    }

    //Fase 1
    private void StartFase1()
    {
        if (!viewCont)
        {
            StartHorde();
            return;
        }

        fase1 = Timer(25); ;
    }

    //Fase 2
    private void StartFase2()
    {
        if (!viewCont)
        {
            StartHorde();
            return;
        }

        fase2 = Timer(35); ;
    }

    //Fase 3
    private void StartFase3()
    {
        if (!viewCont)
        {
            StartHorde();
            return;
        }

        fase3 = finFase = Timer(45);
        
    }

    private bool Timer(float totalTime)
    {
        if (timer < totalTime)
        {
            timer += Time.deltaTime;
            totalIntoTime += Time.deltaTime;
            if(totalIntoTime > 1)
            {
                TextSec--;
                totalIntoTime = 0;
            }
            return false;
        }
        else
        {
            timer = 0;
            SpawnZombie.SetActive(false);
            viewCont = false;
            return true;
        }
    }

    private void StartHorde()
    {
        if (contador < 5)
        {
            contador += Time.deltaTime;
            totalIntoTime += Time.deltaTime;
            if (totalIntoTime > 1)
            {
                TextSec--;
                totalIntoTime = 0;
            }
        }
        else
        {
            contador = 0;
            timer = 0;
            viewCont = true;
            SpawnZombie.SetActive(true);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HordeController : MonoBehaviour
{
    [SerializeField] private HordeTimer TextTimer;
    [SerializeField] private GameObject TextHorde;

    [SerializeField] private TextMeshProUGUI textMesh;
    
    
    [SerializeField] private TextMeshProUGUI PreHorda;

        
    private bool fase1 = false;
    private bool fase2 = false;
    private bool fase3 = false;

    private bool finFase = false;

    [SerializeField] GameObject Ending;

    private float timer = 0;
    private float timer2 = 0;
    private float totalIntoTime = 0;
    private int TextSec = 120;

    private bool viewCont = false;
    private float contador = 0;

    [SerializeField] private GameObject SpawnZombie;
    [SerializeField] private bool level2 = false;

    private void Start()
    {
        //TextTimer.enabled = true;
        TextHorde.SetActive(true);
    }


    private void Update()
    {
        PreHorda.text = "Se acercan... Preparate.";
        if(Timer2())
        {
            PreHorda.text = "";
            TextTimer.setTimer(TextSec);

            textMesh.text = "Resiste la horda.";

            if (!fase1)
                StartFase1();
            else if (!fase2)
                StartFase2();
            else if (!fase3)
                StartFase3();

            if (finFase)
            {
                Ending.SetActive(true);
                TextHorde.SetActive(false);

                if(!level2)
                    textMesh.text = "Ve a la entra del hospital.";
                else
                    textMesh.text = "El auto está listo, sube para escapar.";
            }
        }

    }
    private bool Timer2()
    {
        if (timer2 < 10)
        {
            timer2 += Time.deltaTime;

            return false;
        }
        else
        {
            return true;
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

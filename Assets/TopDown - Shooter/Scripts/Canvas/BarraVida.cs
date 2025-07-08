using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private PlayerVida playerVida;
    private float vidaMaxima;

    void Start()
    {

        var player = GameObject.Find("PlayerRoot").GetComponent<PlayerVida>();
        playerVida = player;
        vidaMaxima = playerVida.vida;
    }


    void Update()
    {
        rellenoBarraVida.fillAmount = playerVida.vida / vidaMaxima;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private PlayerVida playerVida;
    private float vidaMaxima;
    
    void Start()
    {
        playerVida = GameObject.Find("PlayerRoot").GetComponent<PlayerVida>();
        vidaMaxima = playerVida.vida;
    }

    
    void Update()
    {
        rellenoBarraVida.fillAmount = playerVida.vida / vidaMaxima;
    }
}

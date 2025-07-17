using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraSprint : MonoBehaviour
{
    public Image rellenoBarraSprint;
    private PlayerMov player;
    private float tiempoMaximo = 3f;

    void Start()
    {
        
        player = GameObject.Find("PlayerRoot").GetComponent<PlayerMov>();

        
        tiempoMaximo = 3;
    }

    void Update()
    {
        rellenoBarraSprint.fillAmount = player.tiempoSprint / tiempoMaximo;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVida : MonoBehaviour
{
    [SerializeField] public int vida = 4;
    [SerializeField] public MenuGameOver gameOver;

    void Update()
    {
        if(vida < 1)
        {
            Destroy(gameObject);
            gameOver.gameObject.SetActive(true);
        }
    }

    public void AddHead()
    {
        vida = 4;
    }

    public void RecibeDamage(int damage)
    {
        vida -= damage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVida : MonoBehaviour
{
    [SerializeField] public int vida = 4;
    [SerializeField] public MenuGameOver gameOver;
    [SerializeField] public AudioClip death;

    void Update()
    {
        if(vida < 1)
        {
            SoundController.instance.PlaySound(death, 0.5f);
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

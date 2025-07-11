using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVida : MonoBehaviour
{
    [SerializeField] public int vida = 8;
    [SerializeField] public MenuGameOver gameOver;
    [SerializeField] public AudioClip death;
    public int totalPacks = 0;
    [SerializeField] TotalHelthPack packs;

    [SerializeField] private AudioClip audioHealth;

    void Update()
    {
        if(vida < 1)
        {
            SoundController.instance.PlaySound(death, 0.5f);
            Destroy(gameObject);
            gameOver.gameObject.SetActive(true);
        }
        AddHealthAction();

    }

    public void AddHealth()
    {
        vida = 8;
    }

    public void AddHealthAction()
    {
        if(Input.GetKeyDown(KeyCode.H) && vida < 8 && totalPacks > 0)
        {
            AddHealth();
            totalPacks--;
            packs.setPack(totalPacks);
            SoundController.instance.PlaySound(audioHealth, 0.8f);
        }
    }

    public void AddHealthPack()
    {
        totalPacks++;
        packs.AddPack(1);
    }

    public void RecibeDamage(int damage)
    {
        vida -= damage;
    }
}

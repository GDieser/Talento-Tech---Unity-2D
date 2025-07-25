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

    public void AddPack(int total)
    {
        totalPacks = total;
        packs.AddPack(total);
    }

    public void RecibeDamage(int damage)
    {
        vida -= damage;
    }

    public void SetStory(int tipo)
    {
        if(tipo == 1)
            GameStateStory.hablo = true;
        else if(tipo == 2)
            GameStateStory.foto = true;
        else if (tipo == 3)
            GameStateStory.sirena1 = true;
        else if (tipo == 4)
            GameStateStory.sirena2 = true;

    }

    public bool GetStory()
    {
        return GameStateStory.hablo;
    }

    public static class GameStateStory
    {
        public static bool hablo = false;
        public static bool foto = false;
        public static bool sirena1 = false;
        public static bool sirena2 = false;


        public static void Reset()
        {
            hablo = false;
            foto = false;
            sirena1 = false;
            sirena2 = false;
        }

    }
}

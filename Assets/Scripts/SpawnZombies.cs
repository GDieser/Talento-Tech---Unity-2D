using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnZombies : MonoBehaviour
{


    [SerializeField] private GameObject zombie;
    [SerializeField] private Transform player;


    [SerializeField] private float posX;
    [SerializeField] private float posY;

    private int totalZombies = 1;
    private float tiempo = 0;

    [SerializeField] private AudioClip Alert1;
    [SerializeField] private AudioClip Alert2;
    [SerializeField] private AudioClip Alert3;

    private System.Random random = new System.Random();


    void Start()
    {

    }


    void Update()
    {
        if(totalZombies != 0)
        {
            SpawnZombie(totalZombies);
        }

        CoolDownZombies();
        
        
    }

    public void SpawnZombie(int total)
    {
        for (int i = 0; i < total; i++)
        {
            int rand = random.Next(1, 6);

            if (rand == 1)
                SoundController.instance.PlaySound(Alert1, 0.2f);
            else if(rand == 2)
                SoundController.instance.PlaySound(Alert2, 0.2f);
            else if(rand == 3)
                SoundController.instance.PlaySound(Alert3, 0.2f);

            GameObject newZombie = Instantiate(zombie, new Vector2(posX, posY), Quaternion.identity);

            StartCoroutine(DelayedInit(newZombie));
        }
        totalZombies = 0;
    }

    private IEnumerator DelayedInit(GameObject zombie)
    {
        yield return new WaitForFixedUpdate();

        ZombieScriptSpawn zombieSc = zombie.GetComponent<ZombieScriptSpawn>();
        if (zombieSc.player == null)
            zombieSc.player = player;
    }

    private void CoolDownZombies()
    {  
        if(tiempo < 2)
        {
            tiempo += Time.deltaTime;
        }
        else
        {
            tiempo = 0;
            totalZombies = 1;
        }

    }

}

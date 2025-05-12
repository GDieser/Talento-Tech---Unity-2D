using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnZombies : MonoBehaviour
{


    [SerializeField] private GameObject zombie;
    [SerializeField] private Transform player;
    [SerializeField] private Transform Spawn1;

    [SerializeField] private int total = 1;
    private System.Random random = new System.Random();


    void Start()
    {

    }


    void Update()
    {
        while (total > 0)
        {
            SpawnZombie();
            total--;
        }
    }

    private void SpawnZombie()
    {
        for (int i = 0; i < 5; i++)
        {
            int x = random.Next(-25, 25);
            int y = random.Next(-16, 10);

            GameObject newZombie = Instantiate(zombie, new Vector2(x, y), Spawn1.rotation);

            ZombieScript zombieSc = newZombie.GetComponent<ZombieScript>();
            zombieSc.player = player;

        }

    }

}

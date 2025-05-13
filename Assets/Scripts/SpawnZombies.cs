using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnZombies : MonoBehaviour
{


    [SerializeField] private GameObject zombie;
    [SerializeField] private Transform player;


    [SerializeField] private Transform Spawn1;

    private int totalZombies = 5;
    private float tiempo = 0;
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
        Debug.Log("total spawn " + total);

        for (int i = 0; i < total; i++)
        {
            int x = random.Next(-21, 21);
            int y = random.Next(-21, 21);

            GameObject newZombie = Instantiate(zombie, new Vector2(x, y), Quaternion.identity);

            ZombieScript zombieSc = newZombie.GetComponent<ZombieScript>();


            zombieSc.player = player;

        }

        totalZombies = 0;

    }

    private void CoolDownZombies()
    {
        
        if(tiempo < 2)
        {
            Debug.Log(tiempo);
            tiempo += Time.deltaTime;
        }
        else
        {
            tiempo = 0;
            totalZombies = 1;
        }

    }

}

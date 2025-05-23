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
        //Debug.Log("total spawn " + total);

        for (int i = 0; i < total; i++)
        {
            //int x = random.Next(-21, 21);
            //int y = random.Next(-21, 21);

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
            //Debug.Log(tiempo);
            tiempo += Time.deltaTime;
        }
        else
        {
            tiempo = 0;
            totalZombies = 1;
        }

    }

}

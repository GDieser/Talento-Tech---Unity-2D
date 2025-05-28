using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCanva : MonoBehaviour
{
    [SerializeField] private GameObject Ending;
    [SerializeField] private GameObject player;

    private bool animation = false;

    private float timer = 0;


    // Update is called once per frame
    void Update()
    {
        if(animation)
        {
            if(Timer())
            {
                player.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Ending.SetActive(true);
            animation = true;
        }
    }

    private bool Timer()
    {
        if (timer < 5)
        {
            timer += Time.deltaTime;

            return false;
        }
        else
        {
            return true;
        }
    }
}

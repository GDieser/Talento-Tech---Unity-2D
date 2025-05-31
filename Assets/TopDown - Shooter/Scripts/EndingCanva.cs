using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCanva : MonoBehaviour
{
    [SerializeField] private GameObject Ending;
    [SerializeField] private GameObject player;

    private bool animation = false;

    private float timer = 0;

    private void Start()
    {
        Ending.SetActive(true);
        animation = true;
    }


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

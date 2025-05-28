using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HordeCanvaScript : MonoBehaviour
{
    [SerializeField] private GameObject HordeCanva;

    //[SerializeField] private GameObject music;

    [SerializeField] private MusicController music;

    private static bool visto = false;
    private float timer = 0;
    private bool startCanva = false;
    void Start()
    {
        if(visto)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(startCanva && Timer())
        {
            this.gameObject.SetActive(false);
            visto = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            music.isHorde = true;

            HordeCanva.SetActive(true);
            startCanva = true;
        }
    }

    private bool Timer()
    {
        if (timer < 20)
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

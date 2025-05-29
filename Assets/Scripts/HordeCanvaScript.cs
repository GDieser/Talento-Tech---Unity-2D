using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HordeCanvaScript : MonoBehaviour
{
    [SerializeField] private GameObject HordeCanva;
    [SerializeField] private Animator animation;

    [SerializeField] private GameObject Rifle;
    [SerializeField] private MenuPause menu;

    [SerializeField] private MusicController music;

    private static bool visto = false;
    private float timer = 0;
    private bool startCanva = false;
    void Start()
    {
        if(visto)
        {
            Collider2D collider = Rifle.GetComponent<Collider2D>();
            HordeController horde = Rifle.GetComponent<HordeController>();

            collider.enabled = false;
            horde.enabled = true;

            //Rifle.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(startCanva && Timer())
        {
            Collider2D collider = Rifle.GetComponent<Collider2D>();
            HordeController horde = Rifle.GetComponent<HordeController>();

            collider.enabled = false;
            horde.enabled = true;

            this.gameObject.SetActive(false);
            visto = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            menu.SetSpawn();

            animation.enabled = true;
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

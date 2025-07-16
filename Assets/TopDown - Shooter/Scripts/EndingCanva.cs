using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerVida;

public class EndingCanva : MonoBehaviour
{
    [SerializeField] private GameObject Ending;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject hud;

    [SerializeField] private PlayerShotRevolver revolver;
    [SerializeField] private PlayerShotRifle rifle;

    [SerializeField] private PlayerVida packs;


    private bool animation = false;

    private float timer = 0;

    private void Start()
    {
        Ending.SetActive(true);
        hud.SetActive(false);
        animation = true;

        //Debug.Log(rifle.totalBullets);
        //Debug.Log(revolver.totalBullets);
        //Debug.Log(packs.totalPacks);


        //Debug.Log("Guardado en GameManager: " + GameManager.instance.totalRevolverBullets);
        
        GameManager.instance.totalRifleBullets = rifle.totalBullets;
        GameManager.instance.totalPacks = packs.totalPacks;
        GameManager.instance.totalRevolverBullets = revolver.totalBullets;

        GameManager.instance.hablo = GameStateStory.hablo;
        GameManager.instance.sirena1 = GameStateStory.sirena1;
        GameManager.instance.sirena2 = GameStateStory.sirena2;
        
    }


    // Update is called once per frame
    void Update()
    {
        if(animation)
        {
            if(Timer())
            {
                player.SetActive(false);

                MusicController.instance.DetenerMusica();
                SoundController.instance.DetenerFX();

                SceneManager.LoadScene("IntroHospital");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static PlayerMission;

public class AvisoMision : MonoBehaviour
{
    [SerializeField] protected PlayerMission mision;
    [SerializeField] protected TextMeshProUGUI aviso;

    [SerializeField] private MenuPause spawn;

    [SerializeField] private PlayerShotRevolver revolver;
    [SerializeField] private PlayerShotRifle rifle;
    [SerializeField] private PlayerShotShotGun shotGun;

    [SerializeField] private PlayerVida packs;
    private bool shotGunAct = false;

    [SerializeField] private PlayerMov player;

    private bool isInRange = false;

    void Start()
    {
        //Debug.Log("Aviso");
        /*
        GameManager.instance.totalRifleBullets = rifle.totalBullets;
        GameManager.instance.totalRevolverBullets = revolver.totalBullets;
        GameManager.instance.totalPacks = packs.totalPacks;

        GameManager.instance.totalShotGunBullets = shotGun.GetTotalBullets();
        GameManager.instance.shotGun = player.hasShotgun;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void CargarDatosSpawn()
    {
        GameManager.instance.totalRifleBullets = rifle.totalBullets;
        GameManager.instance.totalRevolverBullets = revolver.totalBullets;
        GameManager.instance.totalPacks = packs.totalPacks;

        GameManager.instance.totalShotGunBullets = shotGun.GetTotalBullets();

        GameManager.instance.shotGun = player.hasShotgun;

        GameStateItems.itemMissionStatic1 = true;

        /*
        for (int i = 0; i < 3; i++)
        {
            GameManager.instance.itemMission.Add(true);
        }*/

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = true;

            int cant = mision.itemMission.Count;

            if (cant < 3)
            {
                aviso.text = "Cuidado! Aún te faltan " + (3 - cant) + " Medikits";
            }
            else if(cant == 3)
            {
                CargarDatosSpawn();
                spawn.SetSpawn(3);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            aviso.text = "";
        }
    }


}

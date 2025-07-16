using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static PlayerMission;

public class FinFase1Level2 : MonoBehaviour
{
    [SerializeField] private PlayerMission mision;
    [SerializeField] private TextMeshProUGUI instrucciones;
    [SerializeField] private TextMeshProUGUI intereaccion;

    [SerializeField] private AudioClip door;

    [SerializeField] private GameObject change;
    [SerializeField] private MenuPause spawn;

    [SerializeField] private PlayerMov player;
    [SerializeField] private GameObject playerRoot;

    [SerializeField] private PlayerShotRevolver revolver;
    [SerializeField] private PlayerShotRifle rifle;
    [SerializeField] private PlayerShotShotGun shotGun;

    [SerializeField] private PlayerVida packs;
    private bool shotGunAct = false;


    private bool IsRange = false;
    private bool AllItems = false;

    void Start()
    {
        //Debug.Log("Fase");
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
        if(AllItems && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ChangeFase());
        }
    }

    protected void CargarDatosSpawn()
    {
        GameManager.instance.totalRifleBullets = rifle.totalBullets;
        GameManager.instance.totalRevolverBullets = revolver.totalBullets;
        GameManager.instance.totalPacks = packs.totalPacks;

        GameManager.instance.totalShotGunBullets = shotGun.GetTotalBullets();
        GameManager.instance.shotGun = player.hasShotgun;
    }

    public IEnumerator ChangeFase()
    {
        change.SetActive(true);
        SoundController.instance.PlaySound(door, 0.5f);
        spawn.SetSpawn(2);
        
        yield return new WaitForSeconds(2f);
        playerRoot.SetActive(false);
        player.transform.position = new Vector2(-9, -38f);

        yield return new WaitForSeconds(2f);

        CargarDatosSpawn();
        playerRoot.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        change.SetActive(false);

        this.gameObject.SetActive(false);
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            IsRange = true;

            if((mision.itemMission.Count == 3 || GameStateItems.itemMissionStatic1) && mision.tools )
            {
                intereaccion.text = "Presiona E para salir del hospital";
                AllItems = true;
            }
            else if(mision.itemMission.Count < 3 || !mision.tools)
            {
                intereaccion.text = "Te faltan items para poder abandonar el hospital";
            }

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsRange = false;
            intereaccion.text = "";
        }
    }


}

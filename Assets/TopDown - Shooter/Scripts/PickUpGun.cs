using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class PickUpGun : MonoBehaviour
{
    [SerializeField] private AudioClip audioGun;
    [SerializeField] private PlayerMov player;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerMov>();

            if(this.gameObject.CompareTag("Rifle"))
            {
                collision.GetComponent<PlayerShotRifle>().active = true;
                player.GetComponent<PlayerMov>().DesbloquearRifle();

                SoundController.instance.PlaySound(audioGun, 0.8f);

                player.ChangeGun(2);
                //PlayerShotRifle rifle = GetComponent<PlayerShotRifle>();
                //rifle.active = true;
            }
            else if(this.gameObject.CompareTag("Shotgun"))
            {
                collision.GetComponent<PlayerShotShotGun>().SetActive(true);
                SoundController.instance.PlaySound(audioGun, 0.8f);

                player.ChangeGun(3);
            }
            
            gameObject.SetActive(false);
        }
    }
}
